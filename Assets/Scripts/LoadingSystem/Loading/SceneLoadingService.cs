using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using LoadingSystem.Loading.Operations;
using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.UserState;
using UI;
using UnityEngine;
using Utils;
using Zenject;

namespace LoadingSystem.Loading
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneLoadingService : MonoBehaviour
    {
        private static readonly IInputDelegate.InputRestriction ActionsRestriction = _ => false;

        [field: SerializeField] private CanvasGroup CanvasGroup { get; set; }
        [field: SerializeField] private LoadingProgress ProgressBar { get; set; }
        
        public bool LoadingInProgress { get; private set; }
        
        private readonly List<ISceneLoadingListener> _listeners = new();
        private readonly CancellationTokenSource _destroyCts = new();
        private IGameController _gameController;
        private UserState _userState;
        private IInputDelegate _inputDelegate;
        private HomeSceneLoadingContext _context;

        [Inject]
        private void Construct(
            IGameController gameController,  
            IInputDelegate inputDelegate,
            HomeSceneLoadingContext context)
        {
            _gameController = gameController;
            _userState = _gameController.State.UserState;
            _inputDelegate = inputDelegate;
            _context = context;
            _context.SceneLoadingService = this;
        }

        private void Awake()
        {
            CanvasGroup.alpha = 1f;
            DontDestroyOnLoad(gameObject);

            ChangeLocation(_userState.CurrentLocation, true);
        }
        
        public void ReturnToPreviousLocation() => ChangeLocation(_userState.PreviousLocation);
        
        public void RestartCurrentLocation() => ChangeLocation(_userState.CurrentLocation);

        public void ChangeLocation(LocationState location, bool initial = false) => TravelToLocation(location, initial);

        private void TravelToLocation(LocationState location, bool initial)
        {
            if (!initial && LoadingInProgress)
                return;

            StartLoading();
            _gameController.State.UserState.SetLocation(location);
            _gameController.Save();
            
            var rootSequence = new RootLoadingSequence(CanvasGroup);
            
            if (initial) 
                rootSequence.Add(new GameStartLoadingSequence());

            switch (location.SceneType)
            {
                case LocationState.LocationSceneType.Home:
                    rootSequence.Add(new HomeSceneLoadingSequence(_context));

                    break;
            }

            Load(rootSequence);
        }

        private void Load(RootLoadingSequence sequence)
        {
            gameObject.SetActive(true);
            DoLoad(sequence);
            NotifyListeners(t => t.OnLoadingStarted());
        }

        private async void DoLoad(RootLoadingSequence sequence)
        {
            var loadingSequenceTask = sequence.Load();

            try
            {
                while (!loadingSequenceTask.Status.IsCompleted())
                {
                    var progressValue = sequence.Progress();

                    if (ProgressBar)
                        ProgressBar.SetProgress(progressValue);
                    
                    await UniTask.Yield(cancellationToken: _destroyCts.Token);
                }
            }
            catch
            {
                Debug.Log("[Scene Loading Service] Scene loading has been cancelled.");
                return;
            }
            
            gameObject.SetActive(false);
            CompleteLoading();
            NotifyListeners(t => t.OnLoadingCompleted());
        }

        private void StartLoading()
        {
            LoadingInProgress = true;
            _inputDelegate.AddRestriction(ActionsRestriction);
        }

        private void CompleteLoading()
        {
            LoadingInProgress = false;
            _inputDelegate.RemoveRestriction(ActionsRestriction);
        }

        public void AddListener(ISceneLoadingListener listener)
        {
            if (_listeners.Contains(listener))
                return;

            _listeners.Add(listener);
        }

        public void RemoveListener(ISceneLoadingListener listener) => _listeners.Remove(listener);

        private void NotifyListeners(Action<ISceneLoadingListener> notification)
        {
            foreach (var listener in _listeners) 
                notification.Invoke(listener);
        }

        private void OnDestroy()
        {
            _destroyCts?.Cancel();
            _destroyCts?.Dispose();
        }
    }
    
    public interface ISceneLoadingListener
    {
        void OnLoadingStarted();
        void OnLoadingCompleted();
    }
}
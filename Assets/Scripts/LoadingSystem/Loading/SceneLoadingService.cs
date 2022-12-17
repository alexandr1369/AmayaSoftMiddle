using System;
using System.Collections.Generic;
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
    public class SceneLoadingService : MonoBehaviour
    {
        private static readonly InputDelegate.InputRestriction ActionsRestriction = _ => false;

        [field: SerializeField] private LoadingProgress ProgressBar { get; set; }
        
        public bool LoadingInProgress { get; private set; }
        
        private readonly List<ISceneLoadingListener> _listeners = new();
        private GameController _gameController;
        private UserState _userState;
        private InputDelegate _inputDelegate;
        private HomeSceneLoadingContext _context;
        private CanvasGroup _canvasGroup;

        [Inject]
        private void Construct(
            GameController gameController,  
            InputDelegate inputDelegate,
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
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1f;
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
            
            var rootSequence = new RootLoadingSequence(_canvasGroup);
            
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
            
            while(!loadingSequenceTask.Status.IsCompleted())
            {
                var progressValue = sequence.Progress();
                
                if (ProgressBar) 
                    ProgressBar.SetProgress(progressValue);

                await UniTask.Yield();
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
    }
    
    public interface ISceneLoadingListener
    {
        void OnLoadingStarted();
        void OnLoadingCompleted();
    }
}
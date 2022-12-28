using System;
using System.Collections.Generic;
using DG.Tweening;
using GameItemSystem;
using GameItemSystem.DropGroup;
using LoadingSystem.Loading.Operations.Home;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using Utils;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItem : MonoBehaviour, IConveyorTapeItem
    {
        public event Action OnCollected;
        
        [field: SerializeField] public ConveyorTapeItemConfig Config { get; set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] private List<DropItem> RightAnswerRewards { get; set; }
        
        public LetterItem Item { get; private set; }
        public bool IsBonus { get; private set; }
        
        private IAudioService _audioService;
        private EmptyAnimatedDropGroup _emptyAnimatedDropGroup;
        private HomeSceneLoadingContext _context;
        private IConveyorTapeItemMovable _moveBehaviour;
        private Sequence _interactionAnimationSequence;
        private Vector2 _startScale;
        private Vector2 _collectingScale;

        [Inject]
        public void Construct(
            GameItems gameItems,
            IAudioService audioService,
            HomeSceneLoadingContext context)
        {
            _emptyAnimatedDropGroup = gameItems.GetAsset<EmptyAnimatedDropGroup>();
            _audioService = audioService;
            _context = context;
        }

        private void Awake()
        {
            _startScale = transform.localScale;
            _collectingScale = _startScale * Config.CollectAnimationStartScaleMultiplier;
        }

        public void Init(LetterItem item)
        {
            Item = item;
            IsBonus = false;
            SpriteRenderer.sprite = Item.ConveyorSprite;
            _moveBehaviour = new MovableConveyorTapeItemMoveBehaviour(transform, Config.TapeVelocity);
            transform.localScale = _startScale;
        }

        public void InitBonus(Sprite icon)
        {
            IsBonus = true;
            SpriteRenderer.sprite = icon;
            _moveBehaviour = new MovableConveyorTapeItemMoveBehaviour(transform, Config.TapeVelocity);
            transform.localScale = _startScale;
        }

        private void Update() => _moveBehaviour.Move();

        public void SetMoveBehaviour(IConveyorTapeItemMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        public void PlayDraggingAnimation(bool state)
        {
            if(_interactionAnimationSequence != null && _interactionAnimationSequence.IsPlaying())
                _interactionAnimationSequence.Complete();
            
            var targetScale = state ? _collectingScale : _startScale;
            _interactionAnimationSequence = DOTween.Sequence();
            _interactionAnimationSequence.Append(transform.DOScale(targetScale, 
                Config.CollectAnimationStartScaleDuration));
        }
        
        public void PlayInteractionAnimation(Vector3 targetPosition)
        {
            if(_interactionAnimationSequence != null && _interactionAnimationSequence.IsPlaying())
                _interactionAnimationSequence.Complete();
            
            _interactionAnimationSequence = DOTween.Sequence();
            _interactionAnimationSequence.Append(transform.DOMove(targetPosition, Config.CollectAnimationDuration));
            _interactionAnimationSequence.Join(transform.DOScale(Config.CollectAnimationEndScale, 
                Config.CollectAnimationEndScaleDuration));
            _interactionAnimationSequence.OnComplete(OnInteractionAnimationSequenceComplete);
            _interactionAnimationSequence.Play();
            _audioService.PlayLocalFx(AudioSource, _audioService.InteractionClip);
        }

        private void OnInteractionAnimationSequenceComplete()
        {
            _emptyAnimatedDropGroup.Init(RightAnswerRewards);
            _emptyAnimatedDropGroup.Animate(transform.position, _context.HudItems.Canvas.transform);
            Collect();
        }
        
        public void Collect()
        {
            OnCollected?.Invoke();
            OnCollected = null;
        }
    }

    public interface IConveyorTapeItem
    {
        event Action OnCollected;
        ConveyorTapeItemConfig Config { get; }
        SpriteRenderer SpriteRenderer { get; }
        AudioSource AudioSource { get; }
        LetterItem Item { get; }
        bool IsBonus { get; }
        void Init(LetterItem item);
        void InitBonus(Sprite icon);
        void SetMoveBehaviour(IConveyorTapeItemMovable moveBehaviour);
        void PlayDraggingAnimation(bool state);
        void PlayInteractionAnimation(Vector3 targetPosition);
        void Collect();
    }
}
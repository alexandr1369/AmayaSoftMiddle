using System;
using DG.Tweening;
using GameItemSystem;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItem : MonoBehaviour
    {
        public event Action OnCollected;
        
        [field: SerializeField] public ConveyorTapeItemConfig Config { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        public LetterItem Item { get; private set; }
        public bool IsBonus { get; private set; }
        
        private IConveyorTapeItemMovable _moveBehaviour;
        private Sequence _interactionAnimationSequence;
        private Vector2 _startScale;
        private Vector2 _collectingScale;

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
            _interactionAnimationSequence.OnComplete(Collect);
            _interactionAnimationSequence.Play();
        }
        
        public void Collect()
        {
            OnCollected?.Invoke();
            OnCollected = null;
        }
    }
}
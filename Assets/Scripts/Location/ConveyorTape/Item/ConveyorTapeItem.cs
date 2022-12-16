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
        [field: SerializeField] private SpriteRenderer SpriteRenderer { get; set; }
        
        public LetterItem Item { get; private set; }
        
        private IConveyorTapeItemMovable _moveBehaviour;

        public void Init(LetterItem item)
        {
            Item = item;
            SpriteRenderer.sprite = Item.ConveyorSprite;
            _moveBehaviour = new MovableConveyorTapeItemMoveBehaviour(transform, Config.TapeVelocity);
        }

        private void Update() => _moveBehaviour.Move();

        public void SetMoveBehaviour(IConveyorTapeItemMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        public void PlayInteractionAnimation(Vector3 targetPosition, float duration)
        {
            transform.DOMove(targetPosition, duration)
                .OnComplete(Collect);
        }
        
        public void Collect()
        {
            OnCollected?.Invoke();
            OnCollected = null;
        }
    }
}
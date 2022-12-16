using System;
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
        
        private LetterItem _item;
        private IConveyorTapeItemMovable _moveBehaviour;

        public void Init(LetterItem item)
        {
            _item = item;
            SpriteRenderer.sprite = _item.ConveyorSprite;
            _moveBehaviour = new MovableConveyorTapeItemMoveBehaviour(transform, Config.TapeVelocity);
        }

        private void Update() => _moveBehaviour.Move();

        public void SetMoveBehaviour(IConveyorTapeItemMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        public void Collect()
        {
            // TODO 1): anim

            // TODO 2): OnCollected?.Invoke on animation completion
            
            OnCollected?.Invoke();
            OnCollected = null;
        }
    }
}
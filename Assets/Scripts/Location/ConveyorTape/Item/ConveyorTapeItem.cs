using System;
using GameItemSystem;
using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItem : MonoBehaviour
    {
        public event Action OnCollected;
        
        [field: SerializeField] private SpriteRenderer Renderer { get; set; }
        
        private LetterItem _item;
        private Vector3? _velocity;
        
        public void Init(LetterItem item)
        {
            _item = item;
            Renderer.sprite = _item.ConveyorSprite;
        }

        public void SetVelocity(Vector3 velocity) => _velocity = velocity;

        private void Update()
        {
            if(!_velocity.HasValue)
                return;
            
            transform.position += _velocity.Value * Time.deltaTime;
        }

        public void Collect()
        {
            // TODO 1): anim

            // TODO 2): OnCollected?.Invoke on animation completion

            _velocity = null;
            
            OnCollected?.Invoke();
            OnCollected = null;
        }
    }
}
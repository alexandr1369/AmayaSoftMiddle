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
        
        public void Init(LetterItem item)
        {
            _item = item;
            Renderer.sprite = _item.ConveyorSprite;
        }

        public void Collect()
        {
            // TODO 1): anim

            // TODO 2): OnCollected?.Invoke on animation completion

            OnCollected?.Invoke();
            OnCollected = null;
        }
    }
}
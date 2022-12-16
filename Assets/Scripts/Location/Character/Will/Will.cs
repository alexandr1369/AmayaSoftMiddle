using System;
using GameItemSystem;
using UnityEngine;

namespace Location.Character.Will
{
    public class Will : MonoBehaviour
    {
        public event Action OnWishComeTrue;
     
        [field: SerializeField] private WillConfig Config { get; set; }
        [field: SerializeField] private SpriteRenderer LetterSpriteRenderer { get; set; }

        public LetterItem Item { get; private set; }
        
        public void Init(LetterItem item)
        {
            Item = item;
            LetterSpriteRenderer.sprite = Item.WillSprite;
            LetterSpriteRenderer.color = Config.GetColor();
        }
        
        public void MakeWishComeTrue()
        {
            OnWishComeTrue?.Invoke();
        }
    }
}
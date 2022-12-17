using System;
using GameItemSystem;
using UnityEngine;

namespace Location.Character.Wish
{
    public class Wish : MonoBehaviour
    {
        public event Action OnWishComeTrue;
     
        [field: SerializeField] private WishConfig Config { get; set; }
        [field: SerializeField] private SpriteRenderer LetterSpriteRenderer { get; set; }

        public LetterItem Item { get; private set; }
        
        public void Init(LetterItem item)
        {
            Item = item;
            LetterSpriteRenderer.sprite = Item.WishSprite;
            LetterSpriteRenderer.color = Config.GetColor();
        }
        
        public void MakeWishComeTrue()
        {
            OnWishComeTrue?.Invoke();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using GameItemSystem;
using UnityEngine;
using Zenject;

namespace Location.Character.Wish
{
    public class WishesService
    {
        private readonly List<Wish> _activeWishes = new();
        private GameItems _gameItems;
        private Utils.IFactory<Wish> _factory;

        [Inject]
        private void Construct(GameItems gameItems, Utils.IFactory<Wish> factory)
        {
            _gameItems = gameItems;
            _factory = factory;
        }

        public Wish GetWish()
        {
            var wish = _factory.Create();
            wish.Init(GetAvailableItem());
            wish.OnWishComeTrue += () => wish.Init(GetAvailableItem());
            _activeWishes.Add(wish);
            
            return wish;
        }

        /// <summary>
        /// Wishes can't be repeated with current active ones in characters' clouds.
        /// </summary>
        /// <returns>Non-recurring wish.</returns>
        private LetterItem GetAvailableItem()
        {
            var allItems = _gameItems.GetAssets<LetterItem>();
            
            foreach(var wish in _activeWishes)
                allItems.Remove(wish.Item);
            
            var itemIndex = Random.Range(0, allItems.Count);
            
            return allItems[itemIndex];
        }

        public LetterItem GetRightAnswer()
        {
            var index = Random.Range(0, _activeWishes.Count);
            
            return _activeWishes[index].Item;
        }

        public bool IsRightAnswer(LetterItem item) => _activeWishes.Select(t => t.Item).ToList().IndexOf(item) >= 0;
    }
}
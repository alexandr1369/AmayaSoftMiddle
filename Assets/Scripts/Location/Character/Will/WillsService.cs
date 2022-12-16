using System.Collections.Generic;
using System.Linq;
using GameItemSystem;
using UnityEngine;
using Zenject;

namespace Location.Character.Will
{
    public class WillsService
    {
        private readonly List<Will> _activeWills = new();
        private GameItems _gameItems;
        private Utils.IFactory<Will> _factory;

        [Inject]
        private void Construct(GameItems gameItems, Utils.IFactory<Will> factory)
        {
            _gameItems = gameItems;
            _factory = factory;
        }

        public Will GetWill()
        {
            var will = _factory.Create();
            will.Init(GetAvailableItem());
            will.OnWishComeTrue += () => will.Init(GetAvailableItem());
            _activeWills.Add(will);
            
            return will;
        }

        /// <summary>
        /// Желания не могут повторяться с текущими активными в облачках.
        /// </summary>
        /// <returns>Неповторяющееся желание.</returns>
        private LetterItem GetAvailableItem()
        {
            var allItems = _gameItems.GetAssets<LetterItem>();
            
            foreach(var will in _activeWills)
                allItems.Remove(will.Item);
            
            var itemIndex = Random.Range(0, allItems.Count);
            
            return allItems[itemIndex];
        }

        public LetterItem GetRightAnswer()
        {
            var index = Random.Range(0, _activeWills.Count);
            
            return _activeWills[index].Item;
        }

        public bool IsRightAnswer(LetterItem item) => _activeWills.Select(t => t.Item).ToList().IndexOf(item) >= 0;
    }
}
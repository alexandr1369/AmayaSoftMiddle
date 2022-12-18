using System;
using GameItemSystem;
using UnityEngine;

namespace Backpack
{
    [Serializable]
    public class GameCurrency
    {
        [field: SerializeField] public GameItem Item { get; set; }
        [field: SerializeField] public int Count { get; set; }

        public GameCurrency(GameItem item, int count)
        {
            Item = item;
            Count = count;
        }

        public GameCurrency(GameCurrency currency)
        {
            Item = currency.Item;
            Count = currency.Count;
        }
    }
}
using System;
using System.Collections.Generic;
using GameItemSystem;
using UnityEngine;

namespace Backpack
{
    [CreateAssetMenu(menuName = "AmayaSoft/Item/New Game Init Config", fileName = "GameInitConfig")]
    public class GameInitConfig : ScriptableObject
    {
        [field: SerializeField] public List<ItemCount> ItemCounts { get; private set; }
        [field: SerializeField] public float SubscriptionCountMultiplier { get; private set; }

        [Serializable]
        public struct ItemCount
        {
            [field: SerializeField] public GameItem Item { get; set;}
            [field: SerializeField] public int Count { get; set; }
        }
    }
}
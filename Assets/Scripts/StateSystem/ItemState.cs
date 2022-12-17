using System;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class ItemState
    {
        public const string COIN = "Coin";
        
        [field: SerializeField] public string Id { get; private set; }
        
        public ItemState(string id) => Id = id;
    }
}
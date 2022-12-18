using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameItemSystem.DropGroup
{
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Drop Group", fileName = "DropGroup")]
    public class DropGroup : GameItem
    {
        [field: SerializeField] public List<DropItem> Items { get; set; }
    }

    [Serializable]
    public class DropItem
    {
        [field: SerializeField] public GameItem Item { get; set; }   
        [field: SerializeField] public int Count { get; set; }
    }
}
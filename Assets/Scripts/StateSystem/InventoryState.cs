using System;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class InventoryState
    {
        [field: SerializeField] public bool IsInitialized { get; set; }
    }
}

using System;
using Editor.Tests;
using GameItemSystem;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class InfinityCountedItemState : ItemState
    {
        [field: NonSerialized] public event Action<InfinityCountedItemState> OnChanged;

        [field: SerializeField] public InfinityCount Count { get; private set; }

        public InfinityCountedItemState(string id) : base(id) => Count = new InfinityCount();

        public InfinityCountedItemState(GameItem item) : this(item.name)
        {
        }
        
        public InfinityCount Add(InfinityCount count)
        {
            Count += count;
            
            OnChanged?.Invoke(this);
            
            return Count;
        }

        public InfinityCount Remove(InfinityCount count)
        {
            Count -= count;
            
            OnChanged?.Invoke(this);

            return Count;
        }
    }
}
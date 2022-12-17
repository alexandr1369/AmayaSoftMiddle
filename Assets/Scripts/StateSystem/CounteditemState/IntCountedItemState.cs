using System;
using GameItemSystem;
using UnityEngine;

namespace StateSystem.CounteditemState
{
    [Serializable]
    public class IntCountedItemState : CountedItemState<int>
    {
        public const int NO_CAPACITY = -1;

        [field: NonSerialized] public event Action<IntCountedItemState> OnChanged;

        public IntCountedItemState(string id, int capacity) 
            : base(id, capacity)
        {
        }

        public IntCountedItemState(GameItem item, int capacity) 
            : base(item, capacity)
        {
        }
        
        public override int Add(int count)
        {
            Count = Capacity != NO_CAPACITY
                ? Mathf.Clamp(Count, 0, Capacity)
                : Count + count;
            
            OnChanged?.Invoke(this);
            
            return Count;
        }

        public override int Remove(int count)
        {
            Count = Mathf.Clamp(Count - count, 0, int.MaxValue);
            
            OnChanged?.Invoke(this);
            
            return Count;
        }

    }
}
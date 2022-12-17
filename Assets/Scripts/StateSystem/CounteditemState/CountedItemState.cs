using System;
using GameItemSystem;
using UnityEngine;

namespace StateSystem.CounteditemState
{
    [Serializable]
    public abstract class CountedItemState<T> : ItemState where T : new()
    {
        [field: SerializeField] public T Count { get; protected set; }
        [field: SerializeField] public T Capacity { get; protected set; }

        protected CountedItemState(string id, T capacity) 
            : base(id)
        {
            Count = new T();
            Capacity = capacity;
        }

        protected CountedItemState(GameItem item, T capacity)
            : this(item.name, capacity)
        {
        }

        public abstract T Add(T count);

        public abstract T Remove(T count);
    }
}
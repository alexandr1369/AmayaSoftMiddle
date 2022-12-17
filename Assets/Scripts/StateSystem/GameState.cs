using System;
using System.Collections.Generic;
using System.Linq;
using GameItemSystem;
using StateSystem.CounteditemState;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class GameState
    {
        [field: SerializeField] public UserState.UserState UserState { get; private set; }
        [field: SerializeField] public InventoryState InventoryState { get; private set; }
        [field: SerializeField] public List<IntCountedItemState> IntCountedItemStates { get; private set; }
        [field: SerializeField] public List<AwaitingItemState> AwaitingItemStates { get; private set; }

        /// <summary>
        /// Самая первая инициализация игрового стейта (вызывается единожды после сбрасывания).
        /// </summary>
        public GameState()
        {
            UserState = new UserState.UserState();
            InventoryState = new InventoryState();
            IntCountedItemStates = new List<IntCountedItemState>();
            AwaitingItemStates = new List<AwaitingItemState>();
        }

        public IntCountedItemState GetIntCountedItemStateForItem(GameItem item) => 
            GetIntCountedItemStateForId(item.name);

        public IntCountedItemState GetIntCountedItemStateForId(string id)
        {
            foreach (var state in IntCountedItemStates.Where(state => string.Equals(state.Id, id)))
                return state;

            var newItemState = new IntCountedItemState(id, IntCountedItemState.NO_CAPACITY);
            IntCountedItemStates.Add(newItemState);
            
            return newItemState;
        }
        
        public AwaitingItemState GetAwaitingItemStateForId(string id)
        {
            foreach (var state in AwaitingItemStates.Where(state => string.Equals(state.Id, id)))
                return state;
            
            var newAwaitingItemState = new AwaitingItemState(id);
            AwaitingItemStates.Add(newAwaitingItemState);
            
            return newAwaitingItemState;
        }
    }
}

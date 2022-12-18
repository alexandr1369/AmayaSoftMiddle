using System;
using GameItemSystem;
using LoadingSystem.Loading;
using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.CounteditemState;
using UnityEngine;
using Zenject;

namespace Backpack
{
    public class Inventory : IInitializable, IDisposable, ISceneLoadingListener
    {
        private GameController _gameController;
        private InventoryState _inventoryState;
        private GameInitConfig _gameInitConfig;
        private HomeSceneLoadingContext _context;

        [Inject]
        private void Construct(
            GameController gameController,
            GameInitConfig gameInitConfig,
            HomeSceneLoadingContext context)
        {
            _gameController = gameController;
            _gameInitConfig = gameInitConfig;
            _context = context;
            _context.Inventory = this;
        }

        public void Initialize()
        {
            if(_context.SceneLoadingService)
                _context.SceneLoadingService.AddListener(this);
            
            // <--- start const count
            
            if(_gameController.State.InventoryState.IsInitialized)
                return;
            
            ForceInitConfigsInitialize();
        }

        public void ForceInitConfigsInitialize()
        {
            _gameInitConfig.ItemCounts.ForEach(itemCount =>
            {
                var count = itemCount.Count; 
                var itemState = _gameController.State.GetIntCountedItemStateForItem(itemCount.Item);
                itemState.Add(count);
            });
            
            _gameController.Save();
            _gameController.State.InventoryState.IsInitialized = true;
        }

        public void Add(GameCurrency currency)
        {
            var state = GetItemStateByCurrency(currency);
            state.Add(currency.Count);
            _gameController.Save();
            
            Debug.Log($"[Inventory] Adding {currency.Count} {currency.Item.name} | Total: {state.Count}");
        }

        public void Remove(GameCurrency currency)
        {
            var state = GetItemStateByCurrency(currency);
            state.Remove(currency.Count);
            _gameController.Save();
            
            Debug.Log($"[Inventory] Removing {currency.Count} {currency.Item.name} | Total: {state.Count}");
        }

        private IntCountedItemState GetItemStateByCurrency(GameCurrency currency) => 
            _gameController.State.GetIntCountedItemStateForItem(currency.Item);
        
        public void OnLoadingStarted()
        {
        }

        public void OnLoadingCompleted() => Initialize();

        public void Dispose()
        {
            if(!_context.SceneLoadingService)
                return;
            
            _context.SceneLoadingService.RemoveListener(this);
        }
    }
}
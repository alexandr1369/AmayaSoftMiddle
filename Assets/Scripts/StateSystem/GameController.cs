using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace StateSystem
{
    public class GameController : ITickable
    {
        private const float SAVING_DELAY = .1f;
    
        public GameState State => _gameStateService?.State;
    
        private readonly GameStateService _gameStateService;
        private float? _currentSavingDelay;
    
        public GameController(GameStateService gameStateService, HomeSceneLoadingContext context)
        {
            _gameStateService = gameStateService;
            context.GameController = this;
        }

        public void Save() => _currentSavingDelay = SAVING_DELAY;
    
        public void ClearState() => _gameStateService.ClearState();

        public void Tick()
        {
            if(!_currentSavingDelay.HasValue)
                return;
        
            _currentSavingDelay -= Time.deltaTime;
        
            if(_currentSavingDelay > 0)
                return;
        
            _currentSavingDelay = null;
            _gameStateService.Save();
        }
    }
}
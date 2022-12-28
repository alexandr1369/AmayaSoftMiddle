using UnityEngine;
using Zenject;

namespace StateSystem
{
    public class GameController : IGameController, ITickable
    {
        private const float SAVING_DELAY = .1f;
    
        public GameState State => _gameStateService?.State;
        
        private readonly IGameStateService _gameStateService;

        private float? _currentSavingDelay;

        public GameController(IGameStateService gameStateService) => _gameStateService = gameStateService;

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

    public interface IGameController
    {
        GameState State { get; }
        void Save();
        void ClearState();
    }
}
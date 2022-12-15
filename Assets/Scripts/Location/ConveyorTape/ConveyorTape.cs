using GameItemSystem;
using Location.ConveyorTape.Item;
using UnityEngine;
using Zenject;

namespace Location.ConveyorTape
{
    public class ConveyorTape : MonoBehaviour
    {
        [field: SerializeField] private ConveyorTapeConfig Config { get; set; }
        
        private GameItems _gameItems;
        private Utils.IFactory<ConveyorTapeItem> _factory;
        private Vector3 _startPoint;
        private float _spawningDelay;
        private bool _isActive;

        [Inject]
        private void Construct(GameItems gameItems, Utils.IFactory<ConveyorTapeItem> factory)
        {
            _gameItems = gameItems;
            _factory = factory;
        }

        public void Init(Vector3 startPoint) => _startPoint = startPoint;

        private void Update() => ContinueConveyorTape();

        private void ContinueConveyorTape()
        {
            if(!_isActive)
                return;
            
            _spawningDelay -= Time.deltaTime;
            
            if(_spawningDelay > 0)
                return;
            
            _spawningDelay = Config.SpawningDelay;
            SpawnItem();
        }

        private void SpawnItem()
        {
            var item = _factory.Create();
            item.transform.position = _startPoint;
            
            // // TODO: нельзя создавать одинаковые на сцене объекты - чек где??
            // item.Init();
        }

        // TODO: вынести в ладинг операцию
        public void StartConveyorTape() => _isActive = true;

        public void StopConveyorTape() => _isActive = false;
    }
}
using GameItemSystem;
using Location.Character.Will;
using Location.ConveyorTape.Item;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Location.ConveyorTape
{
    public class ConveyorTape : MonoBehaviour
    {
        [field: SerializeField] public ConveyorTapeConfig Config { get; private set; }
        
        private GameItems _gameItems;
        private WillsService _willsService;
        private Utils.IFactory<ConveyorTapeItem> _factory;
        private Vector3 _startPoint;
        private float _spawningDelay;
        private int _currentIncorrectAnswersInARow;
        private bool _isActive;

        [Inject]
        private void Construct(
            GameItems gameItems,
            WillsService willsService,
            Utils.IFactory<ConveyorTapeItem> factory)
        {
            _gameItems = gameItems;
            _willsService = willsService;
            _factory = factory;
        }

        public void Init(Vector3 startPoint) => _startPoint = startPoint;
        
        // TODO: вынести в ладинг операцию
        public void StartConveyorTape() => _isActive = true;

        public void StopConveyorTape() => _isActive = false;
        
        // TODO: remove
        private void Start() => StartConveyorTape();

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
            
            var letterItems = _gameItems.GetAssets<LetterItem>();
            var randomLetter = letterItems[Random.Range(0, letterItems.Count)];
            var isRightAnswer = _willsService.IsRightAnswer(randomLetter);
            
            if (!isRightAnswer)
                _currentIncorrectAnswersInARow++;
            else
                _currentIncorrectAnswersInARow = 0;
            
            if (_currentIncorrectAnswersInARow > Config.MaxIncorrectAnswersInARow)
            {
                var rightAnswer = _willsService.GetRightAnswer();
                item.Init(rightAnswer);
                _currentIncorrectAnswersInARow = 0;
            }
            else
                item.Init(randomLetter);
        }
    }
}
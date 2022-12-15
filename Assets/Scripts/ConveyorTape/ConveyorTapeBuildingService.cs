using UnityEngine;
using Zenject;

namespace ConveyorTape
{
    public class ConveyorTapeBuildingService : MonoBehaviour
    {
        [field: SerializeField] private ConveyorTapeBuildingServiceConfig Config { get; set; }

        private Camera _homeSceneCamera;
        
        [Inject]
        private void Construct(Camera homeSceneCamera) => _homeSceneCamera = homeSceneCamera;

        private void Start()
        {
            BuildTape();
        }

        public void BuildTape()
        {
            var tapePartPrefab = Config.ConveyorTapePartPrefab;
            var tapePartSize = tapePartPrefab.size * tapePartPrefab.transform.localScale;
            var tapePart1 = Instantiate(tapePartPrefab, transform);
            var tapePart2 = Instantiate(tapePartPrefab, transform);
            Debug.Log($"Conveyor Tape Building Service] Part size: {tapePartSize}");
            
            var tapePartPositionOy = Config.StartPoint.y;
            var startPoint = (Vector2)_homeSceneCamera.ViewportToWorldPoint(Vector2.zero);
            var endPoint = (Vector2)_homeSceneCamera.ViewportToWorldPoint(new Vector2(1, 1));
            startPoint.y = endPoint.y = tapePartPositionOy;
            Debug.Log($"Conveyor Tape Building Service] StartPoint: {startPoint} | EndPoint: {endPoint}");
            
            var tapePartHorizontalOffset = new Vector2(tapePartSize.x / 2f, 0);
            tapePart1.transform.position = startPoint + tapePartHorizontalOffset;
            tapePart2.transform.position = endPoint - tapePartHorizontalOffset;
        }
    }
}
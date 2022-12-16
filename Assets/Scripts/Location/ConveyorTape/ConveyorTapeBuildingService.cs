using UnityEngine;
using Zenject;

namespace Location.ConveyorTape
{
    public class ConveyorTapeBuildingService : MonoBehaviour
    {
        [field: SerializeField] private ConveyorTapeBuildingServiceConfig Config { get; set; }

        private Camera _homeSceneCamera;
        private ConveyorTape _conveyorTape;
        
        [Inject]
        private void Construct(Camera homeSceneCamera, ConveyorTape conveyorTape)
        {
            _homeSceneCamera = homeSceneCamera;
            _conveyorTape = conveyorTape;
        }

        // TODO: remove
        private void Start()
        {
            BuildTape();
        }

        // TODO: вынести в ладинг операцию
        public void BuildTape()
        {
            var tapePartPrefab = Config.ConveyorTapePartPrefab;
            var tapeSidePrefab = Config.ConveyorSidePartPrefab;
            var tapeItemTriggerPrefab = Config.ConveyorTapeItemTriggerPrefab;
            var tapePartSize = tapePartPrefab.size * tapePartPrefab.transform.localScale;
            Debug.Log($"[Conveyor Tape Building Service] Part size: {tapePartSize}");
            
            var tapePartPositionOy = Config.StartPoint.y;
            var startPoint = (Vector2)_homeSceneCamera.ViewportToWorldPoint(Vector2.zero);
            var endPoint = (Vector2)_homeSceneCamera.ViewportToWorldPoint(new Vector2(1, 1));
            startPoint.y = endPoint.y = tapePartPositionOy;
            Debug.Log($"[Conveyor Tape Building Service] StartPoint: {startPoint} | EndPoint: {endPoint}");
            
            var tapePartHorizontalOffset = new Vector2(tapePartSize.x / 2f, 0);
            var lastTapePartPoint = startPoint + tapePartHorizontalOffset;

            while (lastTapePartPoint.x <= endPoint.x)
                SpawnTapePart(tapePartPrefab, transform, ref lastTapePartPoint, tapePartSize.x);

            SpawnTapeSide(tapeSidePrefab, transform, startPoint, Quaternion.Euler(0, 180f, 0));
            SpawnTapeSide(tapeSidePrefab, transform, endPoint, Quaternion.identity);
            SpawnTapeItemTrigger(tapeItemTriggerPrefab, transform, startPoint - tapePartHorizontalOffset);

            _conveyorTape.Init(endPoint);
        }

        private static void SpawnTapePart(
            SpriteRenderer prefab,
            Transform parent,
            ref Vector2 lastTapePartPoint,
            float tapePartSizeOx)
        {
            var tapePart = Instantiate(prefab, parent);
            tapePart.transform.position = lastTapePartPoint;
            lastTapePartPoint.x += tapePartSizeOx;
        }

        private static void SpawnTapeSide(
            SpriteRenderer prefab,
            Transform parent,
            Vector2 tapeSidePoint,
            Quaternion rotation)
        {
            var tapeSide = Instantiate(prefab, parent);
            tapeSide.transform.position = tapeSidePoint;
            tapeSide.transform.rotation = rotation;
        }

        private static void SpawnTapeItemTrigger(
            Collider2D prefab,
            Transform parent,
            Vector3 position)
        {
            var tapeTrigger = Instantiate(prefab, parent);
            tapeTrigger.transform.position = position;
        }
    }
}
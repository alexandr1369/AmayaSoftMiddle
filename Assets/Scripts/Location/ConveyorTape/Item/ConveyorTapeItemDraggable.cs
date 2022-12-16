using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
    {
        [field: SerializeField] private ConveyorTapeItem Item { get; set; }
        
        private Camera _homeSceneCamera;
        private Vector3? _pointerPosition;

        [Inject]
        private void Construct(Camera homeSceneCamera) => _homeSceneCamera = homeSceneCamera;

        private void OnEnable() => _pointerPosition = null;

        private void Update()
        {
            if(!_pointerPosition.HasValue || transform.position == _pointerPosition)
                return;
            
            transform.position = Vector2.Lerp(transform.position, 
                _pointerPosition.Value, Time.deltaTime * Item.Config.DraggingVelocityMultiplier);
        }

        public void OnBeginDrag(PointerEventData eventData) => 
            Item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());

        public void OnDrag(PointerEventData eventData) => 
            _pointerPosition = _homeSceneCamera.ScreenToWorldPoint(eventData.position);

        public void OnEndDrag(PointerEventData eventData)
        {
            Item.SetMoveBehaviour(new MovableConveyorTapeItemMoveBehaviour(
                Item.transform, Item.Config.FallingVelocity));
            _pointerPosition = null;
        }
    }
}
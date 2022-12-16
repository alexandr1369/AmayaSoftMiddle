using UnityEngine;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/ 
    {
        [field: SerializeField] private ConveyorTapeItem Item { get; set; }
        
        private DragDropManager _service;
        private Camera _homeSceneCamera;
        private Touch? _touch;

        [Inject]
        private void Construct(DragDropManager service, Camera homeSceneCamera)
        {
            _service = service;
            _homeSceneCamera = homeSceneCamera;
        }

        private void Update()
        {
            // throw new NotImplementedException();
        }

        // public void OnBeginDrag(PointerEventData eventData) => 
        //     Item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     var pointerPosition = _homeSceneCamera.ScreenToWorldPoint(eventData.position);
        //     transform.position = Vector3.Lerp(transform.position, pointerPosition, Time.deltaTime);
        // }
        //
        // public void OnEndDrag(PointerEventData eventData) => Item.SetMoveBehaviour(
        //     new MovableConveyorTapeItemMoveBehaviour(Item.transform, Item.Config.FallingVelocity));
    }
}
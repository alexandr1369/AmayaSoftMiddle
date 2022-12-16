using System;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour
    {
        [field: SerializeField] private ConveyorTapeItem Item { get; set; }
        
        private TouchesService _service;
        private Touch? _touch;

        [Inject]
        private void Construct(TouchesService service) => _service = service;

        private void Update()
        {
            if(!_touch.HasValue)
                return;

            var touch = _touch.Value;
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());
                    
                    break;
                case TouchPhase.Moved:
                    transform.position += (Vector3)touch.deltaPosition;
                    
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    Item.SetMoveBehaviour(new MovableConveyorTapeItemMoveBehaviour(
                        Item.transform, Item.Config.FallingVelocity));
                    _touch = null;
                    
                    // TODO: check for:
                    
                    // TODO: 1) falling state (pooling)
                    
                    // TODO: 1) interaction animation state (eating + pooling) 
                    
                    break;
            }
        }

        private void OnMouseDown()
        {
            if(!_service.HasFreeTouch())
                return;

            _touch = _service.ReserveTouch();
        }
    }
}
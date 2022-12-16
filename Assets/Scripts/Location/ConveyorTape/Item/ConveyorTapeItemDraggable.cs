using UnityEngine;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour
    {
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
                case TouchPhase.Moved:
                    transform.position += (Vector3)touch.deltaPosition;
                    
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _touch = null;
                    
                    break;
            }
        }

        private void OnMouseDown()
        {
            _touch = _service.ReserveTouch();
            
            Debug.Log("Trying to get touch: " + _touch);
        }
    }
}
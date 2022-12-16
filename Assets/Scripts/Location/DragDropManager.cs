using System;
using System.Collections.Generic;
using Location.ConveyorTape.Item;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using Zenject;

namespace Location
{
    public class DragDropManager : MonoBehaviour
    {
        [field: SerializeField] private LayerMask LayerMask { get; set; }
        
        private readonly Dictionary<int, DraggingObject> _draggingObjects = new();
        private Camera _homeSceneCamera;

        [Inject]
        private void Construct(Camera homeSceneCamera) => _homeSceneCamera = homeSceneCamera;

        private void Awake() => Input.multiTouchEnabled = true;

        private void Update()
        {
            foreach (var touch in Input.touches)
            {
                var ray = _homeSceneCamera.ScreenPointToRay(touch.position);
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    {
                        var hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask);
                        
                        if(!hit)
                            return;
                        
                        if (!_draggingObjects.TryGetValue(touch.fingerId, out var draggingObject))
                        {
                            draggingObject = new DraggingObject();
                            _draggingObjects.Add(draggingObject.FingerID, draggingObject);
                        }
                        
                        draggingObject.FingerID = touch.fingerId;
                        draggingObject.Transform = hit.transform;
                        draggingObject.StartPos = hit.transform.position;
                        draggingObject.DragPlane = new Plane(-_homeSceneCamera.transform.forward, draggingObject.StartPos);
                        
                        draggingObject.DragPlane.Raycast(ray, out var dist);
                        draggingObject.DragOffset = draggingObject.StartPos - ray.GetPoint(dist);
                        
                        var item = hit.transform.GetComponent<ConveyorTapeItem>();
                        item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());

                        break;
                    }
                    case TouchPhase.Moved:
                    {
                        if (_draggingObjects.TryGetValue(touch.fingerId, out var draggingObject))
                        {
                            if (draggingObject.Transform == null)
                            {
                                _draggingObjects.Remove(touch.fingerId);
                                
                                continue;
                            }

                            draggingObject.DragPlane.Raycast(ray, out var dist);
                            draggingObject.Transform.position = ray.GetPoint(dist) + draggingObject.DragOffset;
                        }

                        break;
                    }
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                    {
                        if (_draggingObjects.TryGetValue(touch.fingerId, out var draggingObject))
                        {
                            if (draggingObject.Transform == null)
                                draggingObject.Transform.position = draggingObject.StartPos;
                            
                            _draggingObjects.Remove(touch.fingerId);
                        }

                        break;
                    }
                }
            }
        } 
    }
    
    [Serializable]
    public class DraggingObject
    {
        [field: SerializeField] public Transform Transform { get; set; }
        [field: SerializeField] public int FingerID { get; set; }
        [field: SerializeField] public Vector3 DragOffset { get; set; }
        [field: SerializeField] public Vector3 StartPos { get; set; }
        [field: SerializeField] public Plane DragPlane { get; set; }
    }
}
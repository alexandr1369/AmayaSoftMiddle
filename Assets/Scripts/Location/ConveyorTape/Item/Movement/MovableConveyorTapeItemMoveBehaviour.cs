using UnityEngine;

namespace Location.ConveyorTape.Item.Movement
{
    public class MovableConveyorTapeItemMoveBehaviour : IConveyorTapeItemMovable
    {
        private readonly Transform _transform;
        private readonly Vector3 _velocity;
        
        public MovableConveyorTapeItemMoveBehaviour(Transform transform, Vector3 velocity)
        {
            _transform = transform;
            _velocity = velocity;
        }
        
        public void Move() => _transform.position += _velocity * Time.deltaTime;
    }
}
using UnityEngine;

namespace Location.ConveyorTape
{
    public interface IConveyorTape
    {
        bool IsActive { get; }
        void Init(Vector3 startPoint);
        void StartConveyorTape();
        void StopConveyorTape();
    }
}
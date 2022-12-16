using UnityEngine;

namespace Location.ConveyorTape.Item
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Item Config", fileName = "ConveyorTapeItemConfig")]
    public class ConveyorTapeItemConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 TapeVelocity { get; private set; }
        [field: SerializeField] public Vector2 FallingVelocity { get; private set; }
        [field: SerializeField] public float DraggingVelocityMultiplier { get; private set; }
    }
}
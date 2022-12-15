using UnityEngine;

namespace Location.ConveyorTape
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Building Service Config", fileName = "ConveyorTapeBuildingServiceConfig")]
    public class ConveyorTapeBuildingServiceConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteRenderer ConveyorTapePartPrefab { get; private set; }
        [field: SerializeField] public SpriteRenderer ConveyorSidePartPrefab { get; private set; }
        [field: SerializeField] public Vector3 StartPoint { get; private set; }
    }
}
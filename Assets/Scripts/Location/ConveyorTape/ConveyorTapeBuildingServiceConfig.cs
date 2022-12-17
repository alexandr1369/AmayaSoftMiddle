using UnityEngine;

namespace Location.ConveyorTape
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Building Service Config", fileName = "ConveyorTapeBuildingServiceConfig")]
    public class ConveyorTapeBuildingServiceConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteRenderer ConveyorTapePartPrefab { get; private set; }
        [field: SerializeField] public SpriteRenderer ConveyorSidePartPrefab { get; private set; }
        [field: SerializeField] public Collider2D ConveyorTapeItemTriggerPrefab { get; private set;}
        [field: SerializeField] public Collider2D ConveyorTapeItemBottomTriggerPrefab { get; private set;}
        [field: SerializeField] public float StartPointOy { get; private set; }
        [field: SerializeField] public Vector2 TapeItemTriggerOffset { get; private set; }
        [field: SerializeField] public Vector2 TapeItemSpawnPointOffset { get; private set; }
        [field: SerializeField] public Vector2 TapeItemBottomTriggerOffset { get; private set; }
    }
}
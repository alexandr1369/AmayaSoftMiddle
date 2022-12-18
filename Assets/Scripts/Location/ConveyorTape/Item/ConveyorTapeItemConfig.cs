using UnityEngine;

namespace Location.ConveyorTape.Item
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Item Config", fileName = "ConveyorTapeItemConfig")]
    public class ConveyorTapeItemConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite BonusSprite { get; private set; }
        [field: SerializeField] public Vector2 TapeVelocity { get; set; }
        [field: SerializeField] public Vector2 FallingVelocity { get; private set; }
        [field: SerializeField] public float DraggingVelocity { get; private set; }
        [field: SerializeField] public int DraggingOrderInLayer { get; private set; }

        [field: Header("Collect Animation")]
        [field: SerializeField] public float CollectAnimationDuration { get; private set; }
        [field: SerializeField] public float CollectAnimationStartScaleDuration { get; private set; }
        [field: SerializeField] public float CollectAnimationEndScaleDuration { get; private set; }
        [field: SerializeField] public float CollectAnimationStartScaleMultiplier { get; private set; }
        [field: SerializeField] public Vector2 CollectAnimationEndScale { get; private set; }
    }
}
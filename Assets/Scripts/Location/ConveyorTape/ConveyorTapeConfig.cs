using UnityEngine;

namespace Location.ConveyorTape
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Config", fileName = "ConveyorTapeConfig")]
    public class ConveyorTapeConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawningDelay { get; set; }
        [field: SerializeField] public int MaxIncorrectAnswersInARow { get; set; }
        [field: SerializeField] public Vector2 TapeVelocity { get; set; }
    }
}
using UnityEngine;

namespace Location.ConveyorTape
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Conveyor Tape Config", fileName = "ConveyorTapeConfig")]
    public class ConveyorTapeConfig : ScriptableObject
    {
        private const float MIN_BONUS_ITEM_CHANCE = 0;
        private const float MAX_BONUS_ITEM_CHANCE = 100f;
        
        [field: SerializeField] public float SpawningDelay { get; set; }
        [field: Range(MIN_BONUS_ITEM_CHANCE, MAX_BONUS_ITEM_CHANCE)]
        [field: SerializeField] private float BonusItemChance { get; set; }
        [field: SerializeField] public int MaxIncorrectAnswersInARow { get; set; }

        public bool IsBonus() => Random.Range(MIN_BONUS_ITEM_CHANCE, MAX_BONUS_ITEM_CHANCE) <= BonusItemChance;
    }
}
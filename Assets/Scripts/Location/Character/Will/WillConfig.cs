using System.Collections.Generic;
using UnityEngine;

namespace Location.Character.Will
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Will Config", fileName = "WillConfig")]
    public class WillConfig : ScriptableObject
    {
        [field: SerializeField] public List<Color> LetterColors { get; private set; }

        public Color GetColor()
        {
            var colorIndex = Random.Range(0, LetterColors.Count);
            
            return LetterColors[colorIndex];
        }
    }
}
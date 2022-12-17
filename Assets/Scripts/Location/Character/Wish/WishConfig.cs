using System.Collections.Generic;
using UnityEngine;

namespace Location.Character.Wish
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Wish Config", fileName = "WishConfig")]
    public class WishConfig : ScriptableObject
    {
        [field: SerializeField] public List<Color> LetterColors { get; private set; }

        public Color GetColor()
        {
            var colorIndex = Random.Range(0, LetterColors.Count);
            
            return LetterColors[colorIndex];
        }
    }
}
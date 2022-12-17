using UnityEngine;

namespace Location.Character
{
    [CreateAssetMenu(menuName = "AmayaSoft/Config/New Character Config", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxInteractDistance { get; private set; }
    }
}
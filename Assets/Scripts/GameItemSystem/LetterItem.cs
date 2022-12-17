using UnityEngine;

namespace GameItemSystem
{
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Letter Item", fileName = "LetterItem")]
    public class LetterItem : GameItem
    {
        [field: Header("Letter")]
        [field: SerializeField] public Sprite ConveyorSprite { get; private set; }
        [field: SerializeField] public Sprite WishSprite { get; private set; }
    }
}
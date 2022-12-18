using Backpack;
using UnityEngine;

namespace GameItemSystem
{
    [CreateAssetMenu(menuName = "Zorg/Object/New Shop Item", fileName = "ShopItem", order = 2)]
    public class ShopItem : GameItem
    {
        [field: Header("Settings")]
        [field: SerializeField] public GameCurrency Price { get; private set; }
    }
}
using GameItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudItem : MonoBehaviour
    {
        [field: SerializeField] public GameItem Item { get; private set; }
        [field: SerializeField] private Image ItemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI CountText { get; set; }
        [field: SerializeField] private string OutputFormat { get; set; }
        
        public void Init(GameItem item)
        {
            Item = item;
            ItemImage.sprite = Item.Icon;
        }

        public void SetAmountText(int count) => CountText.text = string.Format(OutputFormat, count);
    }
}
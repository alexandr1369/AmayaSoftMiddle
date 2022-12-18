using GameItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudItem : MonoBehaviour
    {
        [field: SerializeField] public GameItem Item { get; private set; }
        [field: SerializeField] private Image IconImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI AmountText { get; set; }
        [field: SerializeField] private string OutputFormat { get; set; }
        
        public void Init(GameItem item)
        {
            Item = item;
            IconImage.sprite = Item.Icon;
        }

        public void SetAmountText(int count) => AmountText.text = string.Format(OutputFormat, count);
    }
}
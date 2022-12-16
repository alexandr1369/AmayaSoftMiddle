using TMPro;
using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class TouchesCounter : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI Text { get; set; }

        private void Update()
        {
            Text.text = Input.touches.Length.ToString();
        }
    }
}
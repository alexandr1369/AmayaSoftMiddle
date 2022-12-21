using TMPro;
using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class TouchesCounter : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI Text { get; set; }
        [field: SerializeField] private float CheckDelay { get; set; }
        
        private float _currentDelay;
        
        private void Update()
        {
            if (_currentDelay > 0)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }
            
            Text.text = Input.touches.Length.ToString();
            _currentDelay = CheckDelay;
        }
    }
}
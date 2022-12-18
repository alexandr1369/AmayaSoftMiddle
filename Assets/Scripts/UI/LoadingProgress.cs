using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingProgress : MonoBehaviour
    {
        [field: SerializeField] private Image Indicator { get; set; }
        
        public void SetProgress(float value)
        {
            if (!Indicator)
                return;
            
            Indicator.fillAmount = value;
        }
    }
}
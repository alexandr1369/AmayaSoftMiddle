using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class LoadingProgress : MonoBehaviour
    {
        private Image _indicator;

        private void Awake() => _indicator = GetComponent<Image>();

        public void SetProgress(float value)
        {
            if (!_indicator)
                return;
            
            _indicator.fillAmount = value;
        }
    }
}
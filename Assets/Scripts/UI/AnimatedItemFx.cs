using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class AnimatedItemFx : MonoBehaviour
    {
        private Image _image;

        private void Awake() => _image = GetComponent<Image>();
        
        public void Init(Sprite icon) => _image.sprite = icon;
    }
}
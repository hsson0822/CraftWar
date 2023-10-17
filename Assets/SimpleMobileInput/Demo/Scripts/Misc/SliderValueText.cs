using UnityEngine;
using UnityEngine.UI;

namespace SimpleMobileInput.Demo
{
    [RequireComponent(typeof(Slider))]
    public class SliderValueText : MonoBehaviour
    {
        [SerializeField]
        private Text _text = null;

        private Slider _slider = null;
        private const string FORMAT = "F2";

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            UpdateText();
        }

        public void UpdateText()
        {
            if (_text != null && _slider != null)
            {
                _text.text = _slider.value.ToString(FORMAT);
            }
        }
    }
}

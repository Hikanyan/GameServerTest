using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HikanyanLaboratory.Audio
{
    public class VolumeControl : MonoBehaviour, IVolumeBase
    {
        [SerializeField] private TextMeshProUGUI _volumeText;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TMP_InputField _volumeInputField;

        private string _label;
        private float _currentValue;
        private CriAudioManager _criAudioManager; // CriAudioManagerのインスタンスを保持

        public void Initialize(string label, float initialValue, UnityAction<float> onSliderChanged,
            UnityAction<string> onInputChanged)
        {
            _label = label;
            _currentValue = initialValue;
            _criAudioManager = CriAudioManager.Instance;

            _volumeText.text = label;

            _volumeSlider.minValue = 0;
            _volumeSlider.maxValue = 100;
            _volumeSlider.value = initialValue * 100;
            _volumeSlider.onValueChanged.AddListener(onSliderChanged);
            _volumeSlider.onValueChanged.AddListener(OnSliderChanged);

            _volumeInputField.text = (initialValue * 100).ToString(CultureInfo.CurrentCulture);
            _volumeInputField.onEndEdit.AddListener(onInputChanged);
            _volumeInputField.onEndEdit.AddListener(OnInputChanged);
        }

        public void Initialize(string label, float initialValue)
        {
            _label = label;
            _currentValue = initialValue;

            _volumeText.text = label;

            _volumeSlider.minValue = 0;
            _volumeSlider.maxValue = 100;
            _volumeSlider.value = initialValue * 100;
            _volumeSlider.onValueChanged.AddListener(OnSliderChanged);

            _volumeInputField.text = (initialValue * 100).ToString(CultureInfo.CurrentCulture);
            _volumeInputField.onEndEdit.AddListener(OnInputChanged);
        }

        public void SetValue(float value)
        {
            _currentValue = value;
            _volumeSlider.value = value * 100;
            _volumeInputField.text = (value * 100).ToString(CultureInfo.CurrentCulture);
        }

        private void OnSliderChanged(float value)
        {
            _currentValue = value / 100;
            _volumeInputField.text = value.ToString(CultureInfo.CurrentCulture);
            _criAudioManager?.SetVolume(_currentValue);
        }

        private void OnInputChanged(string value)
        {
            if (float.TryParse(value, out float result))
            {
                _currentValue = result / 100;
                _volumeSlider.value = result;
                _criAudioManager?.SetVolume(_currentValue);
            }
        }
    }
}
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HikanyanLaboratory.Audio
{
    public class VolumeControl: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _volumeText;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TMP_InputField _volumeInputField;

        public void Initialize(string label, float initialValue, UnityAction<float> onSliderChanged, UnityAction<string> onInputChanged)
        {
            _volumeText.text = label;
            _volumeSlider.minValue = 0;
            _volumeSlider.maxValue = 100;
            _volumeSlider.value = initialValue * 100;
            _volumeInputField.text = (initialValue * 100).ToString(CultureInfo.CurrentCulture);

            _volumeSlider.onValueChanged.AddListener(onSliderChanged);
            _volumeInputField.onEndEdit.AddListener(onInputChanged);
        }

        public void SetValue(float value)
        {
            _volumeSlider.value = value * 100;
            _volumeInputField.text = (value * 100).ToString(CultureInfo.CurrentCulture);
        }
    }
}
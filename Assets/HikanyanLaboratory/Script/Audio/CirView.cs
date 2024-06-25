﻿using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory
{
    /// <summary>
    /// 各種音声を再生するためのビュー
    /// </summary>
    public class CirView : MonoBehaviour
    {
        // [SerializeField] private Slider _masterVolumeSlider;
        // [SerializeField] private Slider _bgmVolumeSlider;
        // [SerializeField] private Slider _seVolumeSlider;
        // [SerializeField] private Slider _meVolumeSlider;
        // [SerializeField] private InputField _masterVolumeInput;
        // [SerializeField] private InputField _bgmVolumeInput;
        // [SerializeField] private InputField _seVolumeInput;
        // [SerializeField] private InputField _meVolumeInput;
        // [SerializeField] private InputField _bgmCueNameInput;
        // [SerializeField] private InputField _seCueNameInput;
        // [SerializeField] private InputField _meCueNameInput;
        // [SerializeField] private Button _playBgmButton;
        // [SerializeField] private Button _playSeButton;
        // [SerializeField] private Button _playMeButton;
        //
        // private CriAudioPresenter _criAudioManager;
        //
        // private void Start()
        // {
        //     // スライダーの範囲を0～100に設定
        //     _masterVolumeSlider.minValue = 0;
        //     _masterVolumeSlider.maxValue = 100;
        //     _bgmVolumeSlider.minValue = 0;
        //     _bgmVolumeSlider.maxValue = 100;
        //     _seVolumeSlider.minValue = 0;
        //     _seVolumeSlider.maxValue = 100;
        //     _meVolumeSlider.minValue = 0;
        //     _meVolumeSlider.maxValue = 100;
        //
        //     // 初期値設定
        //     _masterVolumeSlider.value = _criAudioManager.MasterVolume * 100;
        //     _bgmVolumeSlider.value = _criAudioManager.BGMVolume * 100;
        //     _seVolumeSlider.value = _criAudioManager.SEVolume * 100;
        //     _meVolumeSlider.value = _criAudioManager.MEVolume * 100;
        //
        //     _masterVolumeInput.text = (_criAudioManager.MasterVolume * 100).ToString(CultureInfo.CurrentCulture);
        //     _bgmVolumeInput.text = (_criAudioManager.BGMVolume * 100).ToString();
        //     _seVolumeInput.text = (_criAudioManager.SEVolume * 100).ToString();
        //     _meVolumeInput.text = (_criAudioManager.MEVolume * 100).ToString();
        //
        //     // イベント登録
        //     _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        //     _bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeSliderChanged);
        //     _seVolumeSlider.onValueChanged.AddListener(OnSeVolumeSliderChanged);
        //     _meVolumeSlider.onValueChanged.AddListener(OnMeVolumeSliderChanged);
        //
        //     _masterVolumeInput.onEndEdit.AddListener(OnMasterVolumeInputChanged);
        //     _bgmVolumeInput.onEndEdit.AddListener(OnBgmVolumeInputChanged);
        //     _seVolumeInput.onEndEdit.AddListener(OnSeVolumeInputChanged);
        //     _meVolumeInput.onEndEdit.AddListener(OnMeVolumeInputChanged);
        //
        //     _playBgmButton.onClick.AddListener(PlayBgm);
        //     _playSeButton.onClick.AddListener(PlaySe);
        //     _playMeButton.onClick.AddListener(PlayMe);
        // }
        //
        // private void OnMasterVolumeSliderChanged(float value)
        // {
        //     _criAudioManager.MasterVolume = value / 100;
        //     _masterVolumeInput.text = value.ToString();
        // }
        //
        // private void OnBgmVolumeSliderChanged(float value)
        // {
        //     _criAudioManager.BGMVolume = value / 100;
        //     _bgmVolumeInput.text = value.ToString();
        // }
        //
        // private void OnSeVolumeSliderChanged(float value)
        // {
        //     _criAudioManager.SEVolume = value / 100;
        //     _seVolumeInput.text = value.ToString();
        // }
        //
        // private void OnMeVolumeSliderChanged(float value)
        // {
        //     _criAudioManager.MEVolume = value / 100;
        //     _meVolumeInput.text = value.ToString();
        // }
        //
        // private void OnMasterVolumeInputChanged(string value)
        // {
        //     if (float.TryParse(value, out float floatValue))
        //     {
        //         _criAudioManager.MasterVolume = floatValue / 100;
        //         _masterVolumeSlider.value = floatValue;
        //     }
        // }
        //
        // private void OnBgmVolumeInputChanged(string value)
        // {
        //     if (float.TryParse(value, out float floatValue))
        //     {
        //         _criAudioManager.BGMVolume = floatValue / 100;
        //         _bgmVolumeSlider.value = floatValue;
        //     }
        // }
        //
        // private void OnSeVolumeInputChanged(string value)
        // {
        //     if (float.TryParse(value, out float floatValue))
        //     {
        //         _criAudioManager.SEVolume = floatValue / 100;
        //         _seVolumeSlider.value = floatValue;
        //     }
        // }
        //
        // private void OnMeVolumeInputChanged(string value)
        // {
        //     if (float.TryParse(value, out float floatValue))
        //     {
        //         _criAudioManager.MEVolume = floatValue / 100;
        //         _meVolumeSlider.value = floatValue;
        //     }
        // }
        //
        // private void PlayBgm()
        // {
        //     string cueName = _bgmCueNameInput.text;
        //     if (!string.IsNullOrEmpty(cueName))
        //     {
        //         _criAudioManager.PlayBGM(cueName);
        //     }
        // }
        //
        // private void PlaySe()
        // {
        //     string cueName = _seCueNameInput.text;
        //     if (!string.IsNullOrEmpty(cueName))
        //     {
        //         _criAudioManager.PlaySE(cueName);
        //     }
        // }
        //
        // private void PlayMe()
        // {
        //     string cueName = _meCueNameInput.text;
        //     if (!string.IsNullOrEmpty(cueName))
        //     {
        //         _criAudioManager.PlayME(cueName);
        //     }
        // }
    }
}
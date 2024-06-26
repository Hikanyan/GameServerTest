using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HikanyanLaboratory.Audio
{
    /// <summary>
    /// 各種音声を再生するためのビュー
    /// </summary>
    public class CirView : MonoBehaviour
    {
        [SerializeField] private GameObject volumeControlPrefab;
        [SerializeField] private GameObject cueNameControlPrefab;
        [SerializeField] private Transform volumeControlsParent;
        [SerializeField] private Transform cueNameControlsParent;
        [SerializeField] private Button _playBgmButton;
        [SerializeField] private Button _playSeButton;
        [SerializeField] private Button _playMeButton;
        [SerializeField] private Button _voiceButton;

        private CriAudioManager _criAudioManager;

        private VolumeControl _masterVolumeControl;
        private VolumeControl _bgmVolumeControl;
        private VolumeControl _seVolumeControl;
        private VolumeControl _meVolumeControl;
        private VolumeControl _voiceVolumeControl;


        private CueNameControl _bgmCueNameControl;
        private CueNameControl _seCueNameControl;
        private CueNameControl _meCueNameControl;
        private CueNameControl _voiceCueNameControl;


        private float _bgmVolume = 1f;
        private float _seVolume = 1f;
        private float _meVolume = 1f;
        private float _voiceVolume = 1f;

        private void Start()
        {
            _criAudioManager = CriAudioManager.Instance;

            _masterVolumeControl = CreateVolumeControl("Master Volume", _criAudioManager.MasterVolume,
                OnMasterVolumeSliderChanged, OnMasterVolumeInputChanged);
            _bgmVolumeControl = CreateVolumeControl("BGM Volume", _bgmVolume, OnBgmVolumeSliderChanged,
                OnBgmVolumeInputChanged);
            _seVolumeControl =
                CreateVolumeControl("SE Volume", _seVolume, OnSeVolumeSliderChanged, OnSeVolumeInputChanged);
            _meVolumeControl =
                CreateVolumeControl("ME Volume", _meVolume, OnMeVolumeSliderChanged, OnMeVolumeInputChanged);
            _voiceVolumeControl =
                CreateVolumeControl("Voice Volume", _voiceVolume, OnVoiceVolumeSliderChanged, OnVoiceVolumeInputChanged);
            
            _bgmCueNameControl = CreateCueNameControl("BGM Cue Name");
            _seCueNameControl = CreateCueNameControl("SE Cue Name");
            _meCueNameControl = CreateCueNameControl("ME Cue Name");
            _voiceCueNameControl = CreateCueNameControl("Voice Cue Name");

            _playBgmButton.onClick.AddListener(PlayBgm);
            _playSeButton.onClick.AddListener(PlaySe);
            _playMeButton.onClick.AddListener(PlayMe);
            _voiceButton.onClick.AddListener(PlayVoice);
        }

        private VolumeControl CreateVolumeControl(string label, float initialValue, UnityAction<float> onSliderChanged,
            UnityAction<string> onInputChanged)
        {
            if (volumeControlPrefab == null)
            {
                UnityEngine.Debug.LogError("volumeControlPrefab is not assigned.");
                return null;
            }
            var volumeControlObject = Instantiate(volumeControlPrefab, volumeControlsParent);
            var volumeControl = volumeControlObject.GetComponent<VolumeControl>();
            volumeControl.Initialize(label, initialValue, onSliderChanged, onInputChanged);
            return volumeControl;
        }

        private CueNameControl CreateCueNameControl(string label)
        {
            var cueNameControlObject = Instantiate(cueNameControlPrefab, cueNameControlsParent);
            var cueNameControl = cueNameControlObject.GetComponent<CueNameControl>();
            cueNameControl.Initialize(label);
            return cueNameControl;
        }

        private void OnMasterVolumeSliderChanged(float value)
        {
            _criAudioManager.MasterVolume = value / 100;
            _masterVolumeControl.SetValue(value / 100);
        }

        private void OnBgmVolumeSliderChanged(float value)
        {
            _bgmVolume = value / 100;
            _bgmVolumeControl.SetValue(value / 100);
            UpdateBGMVolume();
        }

        private void OnSeVolumeSliderChanged(float value)
        {
            _seVolume = value / 100;
            _seVolumeControl.SetValue(value / 100);
            UpdateSEVolume();
        }

        private void OnMeVolumeSliderChanged(float value)
        {
            _meVolume = value / 100;
            _meVolumeControl.SetValue(value / 100);
            UpdateMEVolume();
        }
        
        private void OnVoiceVolumeSliderChanged(float value)
        {
            _voiceVolume = value / 100;
            _voiceVolumeControl.SetValue(value / 100);
            UpdateVoiceVolume();
        }

        private void OnMasterVolumeInputChanged(string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                _criAudioManager.MasterVolume = floatValue / 100;
                _masterVolumeControl.SetValue(floatValue / 100);
            }
        }

        private void OnBgmVolumeInputChanged(string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                _bgmVolume = floatValue / 100;
                _bgmVolumeControl.SetValue(floatValue / 100);
                UpdateBGMVolume();
            }
        }

        private void OnSeVolumeInputChanged(string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                _seVolume = floatValue / 100;
                _seVolumeControl.SetValue(floatValue / 100);
                UpdateSEVolume();
            }
        }

        private void OnMeVolumeInputChanged(string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                _meVolume = floatValue / 100;
                _meVolumeControl.SetValue(floatValue / 100);
                UpdateMEVolume();
            }
        }
        
        private void OnVoiceVolumeInputChanged(string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                _voiceVolume = floatValue / 100;
                _voiceVolumeControl.SetValue(floatValue / 100);
                UpdateVoiceVolume();
            }
        }

        private void UpdateBGMVolume()
        {
            var players = _criAudioManager.GetPlayers(CriAudioType.BGM);
            foreach (var player in players)
            {
                player.SetVolume(_bgmVolume);
                // プレイバックを更新する必要がある場合はここで更新
                foreach (var playerData in _criAudioManager.GetPlayerData(CriAudioType.BGM))
                {
                    player.Update(playerData.Playback);
                }
            }
        }

        private void UpdateSEVolume()
        {
            var players = _criAudioManager.GetPlayers(CriAudioType.SE);
            foreach (var player in players)
            {
                player.SetVolume(_seVolume);
                // プレイバックを更新する必要がある場合はここで更新
                foreach (var playerData in _criAudioManager.GetPlayerData(CriAudioType.SE))
                {
                    player.Update(playerData.Playback);
                }
            }
        }

        private void UpdateMEVolume()
        {
            var players = _criAudioManager.GetPlayers(CriAudioType.ME);
            foreach (var player in players)
            {
                player.SetVolume(_meVolume);
                // プレイバックを更新する必要がある場合はここで更新
                foreach (var playerData in _criAudioManager.GetPlayerData(CriAudioType.ME))
                {
                    player.Update(playerData.Playback);
                }
            }
        }
        
        private void UpdateVoiceVolume()
        {
            var players = _criAudioManager.GetPlayers(CriAudioType.Voice);
            foreach (var player in players)
            {
                player.SetVolume(_voiceVolume);
                // プレイバックを更新する必要がある場合はここで更新
                foreach (var playerData in _criAudioManager.GetPlayerData(CriAudioType.Voice))
                {
                    player.Update(playerData.Playback);
                }
            }
        }

        private void PlayBgm()
        {
            string cueName = _bgmCueNameControl.GetCueName();
            if (!string.IsNullOrEmpty(cueName))
            {
                _criAudioManager.Play(CriAudioType.BGM, cueName);
            }
        }

        private void PlaySe()
        {
            string cueName = _seCueNameControl.GetCueName();
            if (!string.IsNullOrEmpty(cueName))
            {
                _criAudioManager.Play(CriAudioType.SE, cueName);
            }
        }

        private void PlayMe()
        {
            string cueName = _meCueNameControl.GetCueName();
            if (!string.IsNullOrEmpty(cueName))
            {
                _criAudioManager.Play(CriAudioType.ME, cueName);
            }
        }
        
        private void PlayVoice()
        {
            string cueName = _voiceCueNameControl.GetCueName();
            if (!string.IsNullOrEmpty(cueName))
            {
                _criAudioManager.Play(CriAudioType.Voice, cueName);
            }
        }
    }
}
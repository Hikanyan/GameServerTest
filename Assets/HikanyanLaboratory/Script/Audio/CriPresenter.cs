using UnityEngine.Events;

namespace HikanyanLaboratory.Audio
{
    public class CriPresenter
    {
        private readonly CriAudioManager _criAudioManager;
        private float _bgmVolume = 1f;
        private float _seVolume = 1f;
        private float _meVolume = 1f;
        private float _voiceVolume = 1f;
        
        public CriPresenter(CriAudioManager criAudioManager)
        {
            _criAudioManager = criAudioManager;
        }

        public void InitializeVolumeControl(VolumeControl volumeControl, CriAudioType audioType)
        {
            float initialValue = GetVolume(audioType);
            volumeControl.Initialize(audioType.ToString(), initialValue, 
                value => OnVolumeSliderChanged(audioType, value), 
                value => OnVolumeInputChanged(audioType, value));
        }
        public void InitializeCueNameControl(CueNameControl cueNameControl, string label)
        {
            cueNameControl.Initialize(label);
        }

        private void OnVolumeSliderChanged(CriAudioType audioType, float value)
        {
            SetVolume(audioType, value / 100);
        }

        private void OnVolumeInputChanged(CriAudioType audioType, string value)
        {
            if (float.TryParse(value, out float floatValue))
            {
                SetVolume(audioType, floatValue / 100);
            }
        }

        public void SetVolume(CriAudioType audioType, float value)
        {
            switch (audioType)
            {
                case CriAudioType.Master:
                    _criAudioManager.MasterVolume = value;
                    break;
                case CriAudioType.BGM:
                    _bgmVolume = value;
                    UpdateVolume(_criAudioManager.GetPlayerData(CriAudioType.BGM), value);
                    break;
                case CriAudioType.SE:
                    _seVolume = value;
                    UpdateVolume(_criAudioManager.GetPlayerData(CriAudioType.SE), value);
                    break;
                case CriAudioType.ME:
                    _meVolume = value;
                    UpdateVolume(_criAudioManager.GetPlayerData(CriAudioType.ME), value);
                    break;
                
                    break;
            }
        }

        private float GetVolume(CriAudioType audioType)
        {
            return audioType switch
            {
                CriAudioType.BGM => _criAudioManager.BGMVolume,
                CriAudioType.SE => _criAudioManager.SEVolume,
                CriAudioType.ME => _criAudioManager.MEVolume,
                CriAudioType.Master => _criAudioManager.MasterVolume,
                _ => 1f
            };
        }

        private void UpdateVolume(System.Collections.Generic.List<CriAudioManager.CriPlayerData> playerDataList,
            float volume)
        {
            foreach (var playerData in playerDataList)
            {
                playerData.Playback.SetVolume(volume);
            }
        }

        public void Play(CriAudioType audioType, string cueName)
        {
            _criAudioManager.Play(audioType, cueName);
        }
    }
}
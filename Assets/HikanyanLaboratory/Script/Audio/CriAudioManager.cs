using System;
using System.Collections.Generic;
using CriWare;
using HikanyanLaboratory.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioManager : AbstractSingletonMonoBehaviour<CriAudioManager>
    {
        [SerializeField] private CriAudioSetting _audioSetting;
        private float _masterVolume = 1F; // マスターボリューム
        private const float Diff = 0.01F; // 音量の変更があったかどうかの判定に使う

        public Action<float> MasterVolumeChanged; // マスターボリューム変更時のイベント
        private Dictionary<CriAudioType, ICriAudioPlayerService> _audioPlayers; // 各音声の再生を管理するクラス

        private CriAtomListener _listener; // リスナー
        protected override bool UseDontDestroyOnLoad => true;
        public CriAudioType CriAudioType { get; set; }

        private void Awake()
        {
            // ACF設定
            string path = Application.streamingAssetsPath + $"/{_audioSetting.StreamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);

            // CriAtom作成
            gameObject.AddComponent<CriAtom>();

            _listener = FindObjectOfType<CriAtomListener>();
            if (_listener == null)
            {
                _listener = gameObject.AddComponent<CriAtomListener>();
            }

            _audioPlayers = new Dictionary<CriAudioType, ICriAudioPlayerService>();

            foreach (var cueSheet in _audioSetting.AudioCueSheet)
            {
                CriAtom.AddCueSheet(cueSheet.CueSheetName, cueSheet.AcbPath, cueSheet.AwbPath, null);
                if (cueSheet.CueSheetName == CriAudioType.CueSheet_BGM.ToString())
                {
                    _audioPlayers.Add(CriAudioType.CueSheet_BGM, new BGMPlayer(cueSheet.CueSheetName, _listener));
                }
                else if (cueSheet.CueSheetName == CriAudioType.CueSheet_SE.ToString())
                {
                    _audioPlayers.Add(CriAudioType.CueSheet_SE, new SEPlayer(cueSheet.CueSheetName, _listener));
                }
                else if (cueSheet.CueSheetName == CriAudioType.CueSheet_Voice.ToString())
                {
                    _audioPlayers.Add(CriAudioType.CueSheet_Voice, new VoicePlayer(cueSheet.CueSheetName, _listener));
                }
                else if (cueSheet.CueSheetName == CriAudioType.CueSheet_ME.ToString())
                {
                    _audioPlayers.Add(CriAudioType.CueSheet_ME, new MEPlayer(cueSheet.CueSheetName, _listener));
                }
                else if (cueSheet.CueSheetName == CriAudioType.Other.ToString())
                {
                    _audioPlayers.Add(CriAudioType.Other, new OtherPlayer(cueSheet.CueSheetName, _listener));
                }
                
                // 他のCriAudioTypeも同様に追加可能
            }

            MasterVolumeChanged += volume =>
            {
                foreach (var player in _audioPlayers)
                {
                    player.Value.SetVolume(volume);
                }
            };

            SceneManager.sceneUnloaded += Unload;
        }

        private void OnDestroy()
        {
            SceneManager.sceneUnloaded -= Unload;
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                if (!(_masterVolume + Diff < value) && !(_masterVolume - Diff > value)) return;
                MasterVolumeChanged?.Invoke(value);
                _masterVolume = value;
            }
        }

        public Player Play(CriAudioType type, string cueName)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                return new Player(player, cueName);
            }
            else
            {
                Debug.LogWarning($"Audio type {type} not supported.");
                return null;
            }
        }

        public Player Play3D(GameObject gameObject, CriAudioType type, string cueName)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                return new Player(player, cueName); // Adjust the Player class for 3D if needed
            }
            else
            {
                Debug.LogWarning($"3D audio type {type} not supported.");
                return null;
            }
        }

        private void Unload(Scene scene)
        {
            foreach (var player in _audioPlayers.Values)
            {
                player.Dispose();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using CriWare;
using HikanyanLaboratory.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioManager : AbstractSingletonMonoBehaviour<CriAudioManager>
    {
        [SerializeField] private CriAudioSetting _audioSetting;
        private float _masterVolume = 1F; // マスターボリューム
        private const float Diff = 0.01F; // 音量の変更があったかどうかの判定に使う

        private readonly ISubject<float> _masterVolumeChanged = new Subject<float>(); // マスターボリューム変更時のイベント
        private Dictionary<CriAudioType, ICriAudioPlayerService> _audioPlayers; // 各音声の再生を管理するクラス

        private CriAtomListener _listener; // リスナー
        protected override bool UseDontDestroyOnLoad => true;

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
                CriAtom.AddCueSheet(cueSheet.CueSheetName, $"{cueSheet.AcbPath}.acb",
                    !string.IsNullOrEmpty(cueSheet.AwbPath) ? $"{cueSheet.AwbPath}.awb" : null, null);
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

            _masterVolumeChanged.Subscribe(volume =>
            {
                foreach (var player in _audioPlayers.Values)
                {
                    player.SetVolume(volume);
                }
            }).AddTo(this);

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
                if (Math.Abs(_masterVolume - value) < Diff) return;
                _masterVolumeChanged.OnNext(value);
                _masterVolume = value;
            }
        }

        public Guid Play(CriAudioType type, string cueName)
        {
            return Play(type, cueName, 1f, false);
        }

        public Guid Play(CriAudioType type, string cueName, bool isLoop)
        {
            return Play(type, cueName, 1f, isLoop);
        }

        public Guid Play(CriAudioType type, string cueName, float volume, bool isLoop = false)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                Debug.Log($"CriAudioType: {type}, CueName: {cueName}");
                return player.Play(cueName, volume, isLoop);
            }
            else
            {
                Debug.LogWarning($"Audio type {type} not supported.");
                return Guid.Empty;
            }
        }

        public Guid Play3D(Transform transform, CriAudioType type, string cueName)
        {
            return Play3D(transform, type, cueName, 1f, false);
        }

        public Guid Play3D(Transform transform, CriAudioType type, string cueName, bool isLoop)
        {
            return Play3D(transform, type, cueName, 1f, isLoop);
        }

        public Guid Play3D(Transform transform, CriAudioType type, string cueName, float volume, bool isLoop = false)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                Debug.Log($"CriAudioType: {type}, CueName: {cueName}");
                return player.Play3D(transform, cueName, volume, isLoop);
            }
            else
            {
                Debug.LogWarning($"3D audio type {type} not supported.");
                return Guid.Empty;
            }
        }

        public void Stop(CriAudioType type, Guid id)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                player.Stop(id);
            }
            else
            {
                Debug.LogWarning($"Audio type {type} not supported.");
            }
        }

        public void Pause(CriAudioType type, Guid id)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                player.Pause(id);
            }
            else
            {
                Debug.LogWarning($"Audio type {type} not supported.");
            }
        }

        public void Resume(CriAudioType type, Guid id)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                player.Resume(id);
            }
            else
            {
                Debug.LogWarning($"Audio type {type} not supported.");
            }
        }

        public void StopAll()
        {
            foreach (var player in _audioPlayers.Values)
            {
                player.StopAll();
            }
        }

        public void PauseAll()
        {
            foreach (var player in _audioPlayers.Values)
            {
                player.PauseAll();
            }
        }

        public void ResumeAll()
        {
            foreach (var player in _audioPlayers.Values)
            {
                player.ResumeAll();
            }
        }

        public List<ICriAudioPlayerService> GetPlayers(CriAudioType type)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                return new List<ICriAudioPlayerService> { player };
            }

            return new List<ICriAudioPlayerService>();
        }

        private void Unload(Scene scene)
        {
            foreach (var player in _audioPlayers.Values)
            {
                player.Dispose();
            }
        }

        public void Dispose()
        {
            SceneManager.sceneUnloaded -= Unload;
            foreach (var player in _audioPlayers.Values)
            {
                player.Dispose();
            }
        }
    }
}

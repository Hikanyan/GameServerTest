using System;
using System.Collections;
using System.Collections.Generic;
using CriWare;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioManager : AbstractSingletonMonoBehaviour<CriAudioManager>
    {
        [SerializeField] private string _streamingAssetsPathAcf = "Chronicle Dimention"; //.acf
        [SerializeField] private List<AudioCueSheet<CriAudioType>> _cueSheets;

        private float _masterVolume = 1F; //マスターボリューム
        private const float Diff = 0.01F; //音量の変更があったかどうかの判定に使う

        public Action<float> MasterVolumeChanged; //マスターボリューム変更時のイベント
        private Dictionary<CriAudioType, ICriAudioPlayer> _audioPlayers;//各音声の再生を管理するクラス

        private CriAtomEx3dSource _3dSource;//3D音源
        private CriAtomListener _listener;//リスナー
        protected override bool UseDontDestroyOnLoad => true;


        private void Awake()
        {
            // ACF設定
            string path = Application.streamingAssetsPath + $"/{_streamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);

            // CriAtom作成
            transform.gameObject.AddComponent<CriAtom>();

            _3dSource = new CriAtomEx3dSource();
            _3dSource.SetPosition(0, 0, 0);
            _3dSource.Update();

            _listener = FindObjectOfType<CriAtomListener>();
            if (_listener == null)
            {
                UnityEngine.Debug.LogWarning($"{nameof(CriAtomListener)} が見つかりません。");
            }

            _audioPlayers = new Dictionary<CriAudioType, ICriAudioPlayer>();

            foreach (var cueSheet in _cueSheets)
            {
                CriAtom.AddCueSheet(cueSheet.CueSheetName, cueSheet.AcbPath, cueSheet.AwbPath, null);
                _audioPlayers[cueSheet.Type] = new CriAudioPlayer(cueSheet.CueSheetName, _3dSource, _listener);
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

        public void Play(CriAudioType type, string cueName, float volume = 1f, bool isLoop = false)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                player.Play(cueName, volume, isLoop);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Audio type {type} not supported.");
            }
        }

        public void Play3D(GameObject gameObject, CriAudioType type, string cueName, float volume = 1f,
            bool isLoop = false)
        {
            if (_audioPlayers.TryGetValue(type, out var player))
            {
                player.Play3D(gameObject, cueName, volume, isLoop);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"3D audio type {type} not supported.");
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
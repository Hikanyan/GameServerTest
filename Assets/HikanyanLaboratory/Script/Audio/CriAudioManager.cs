using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CriWare;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory
{
    public class AudioManager : IDisposable
    {
        private readonly AudioSettings _settings;
        public ICriAudio BGM { get; }
        public ICriAudio SE { get; }
        public ICriAudio ME { get; }

        private float _masterVolume = 1f;
        private readonly Subject<float> _masterVolumeChanged = new Subject<float>();
        private readonly Dictionary<CueSheet, string> _cueSheetPaths = new Dictionary<CueSheet, string>();
        private readonly Dictionary<CueSheet, string> _awbPaths = new Dictionary<CueSheet, string>();

        public AudioManager(AudioSettings settings, BgmCueSheet bgmCueSheet, SeCueSheet seCueSheet,
            MeCueSheet meCueSheet)
        {
            _settings = settings;
            BGM = bgmCueSheet;
            SE = seCueSheet;
            ME = meCueSheet;

            foreach (var cueSheetPath in _settings.cueSheetPaths)
            {
                _cueSheetPaths[cueSheetPath._cueSheet] = cueSheetPath._path;
            }

            foreach (var awbPath in _settings.AwbPaths)
            {
                _awbPaths[awbPath._cueSheet] = awbPath._path;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public async UniTask Initialize()
        {
            string path = Application.streamingAssetsPath + $"/{_settings.streamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);

            foreach (var sheet in _cueSheetPaths.Keys)
            {
                await AddCueSheet(sheet, _cueSheetPaths[sheet]);
            }

            var listener = UnityEngine.Object.FindObjectOfType<CriAtomListener>();
            if (listener != null)
            {
                BGM.Set3dListener(listener.nativeListener);
                SE.Set3dListener(listener.nativeListener);
                ME.Set3dListener(listener.nativeListener);
            }
        }

        /// <summary>
        /// キューシートを追加
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cueSheetName"></param>
        private async UniTask AddCueSheet(CueSheet sheet, string cueSheetName)
        {
            string awbPath = _awbPaths.ContainsKey(sheet) && !string.IsNullOrEmpty(_awbPaths[sheet])
                ? $"{_awbPaths[sheet]}.awb"
                : null;
            CriAtom.AddCueSheet(cueSheetName, $"{cueSheetName}.acb", awbPath, null);
            await UniTask.Yield();
        }

        /// <summary>
        /// マスターボリュームを設定
        /// </summary>
        /// <param name="volume"></param>
        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                _masterVolumeChanged.OnNext(value);
                UpdateAllVolumes();
            }
        }

        /// <summary>
        /// 全てのボリュームを更新
        /// </summary>
        private void UpdateAllVolumes()
        {
            BGM.Volume(_masterVolume);
            SE.Volume(_masterVolume);
            ME.Volume(_masterVolume);
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            BGM.Dispose();
            SE.Dispose();
            ME.Dispose();
        }
    }
}
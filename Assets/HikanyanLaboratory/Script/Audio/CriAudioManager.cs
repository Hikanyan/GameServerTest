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
        public Subject<float> MasterVolumeChanged = new Subject<float>();

        public AudioManager(AudioSettings settings, BgmCueSheet bgmCueSheet, SeCueSheet seCueSheet, MeCueSheet meCueSheet)
        {
            _settings = settings;
            BGM = bgmCueSheet;
            SE = seCueSheet;
            ME = meCueSheet;
        }

        public async UniTask Initialize()
        {
            string path = Application.streamingAssetsPath + $"/{_settings.StreamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);

            foreach (var sheet in _settings.CueSheetPaths.Keys)
            {
                await AddCueSheet(sheet, _settings.CueSheetPaths[sheet]);
            }

            var listener = UnityEngine.Object.FindObjectOfType<CriAtomListener>();
            if (listener != null)
            {
                BGM.Player.Set3dListener(listener.nativeListener);
                SE.Player.Set3dListener(listener.nativeListener);
                ME.Player.Set3dListener(listener.nativeListener);
            }
        }

        private async UniTask AddCueSheet(CueSheet sheet, string cueSheetName)
        {
            CriAtom.AddCueSheet(cueSheetName, $"{cueSheetName}.acb", null, null);
            await UniTask.Yield();
        }

        public void SetMasterVolume(float volume)
        {
            _masterVolume = volume;
            MasterVolumeChanged.OnNext(volume);
            UpdateAllVolumes();
        }

        private void UpdateAllVolumes()
        {
            BGM.Player.SetVolume(BGM.VolumeLevel * _masterVolume);
            SE.Player.SetVolume(SE.VolumeLevel * _masterVolume);
            ME.Player.SetVolume(ME.VolumeLevel * _masterVolume);
        }

        public void Dispose()
        {
            BGM.Dispose();
            SE.Dispose();
            ME.Dispose();
        }
    }
}
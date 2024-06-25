using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    public class AudioLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioSettings _audioSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            if (_audioSettings == null)
            {
                UnityEngine.Debug.LogError("AudioSettings is null");
            }

            builder.RegisterInstance(_audioSettings);
            var cueSheetDictionary = new Dictionary<CueSheet, string>();
            foreach (var cueSheetPath in _audioSettings.cueSheetPaths)
            {
                cueSheetDictionary[cueSheetPath._cueSheet] = cueSheetPath._path;
            }

            var awbDictionary = new Dictionary<CueSheet, string>();
            foreach (var awbPath in _audioSettings.AwbPaths)
            {
                awbDictionary[awbPath._cueSheet] = awbPath._path;
            }


            builder.Register<BgmCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<SeCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<MeCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<AudioManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CriAudioEditor>(Lifetime.Singleton);
            builder.Register<CriAudioPresenter>(Lifetime.Singleton);
        }
    }
}
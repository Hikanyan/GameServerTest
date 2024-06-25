using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.LifeTimeScope
{
    public class AudioLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioSettings _audioSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_audioSettings);
            builder.Register<BgmCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<SeCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<MeCueSheet>(Lifetime.Singleton).As<ICriAudio>();
            builder.Register<AudioManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CriAudioPresenter>(Lifetime.Singleton);
        }
    }
}
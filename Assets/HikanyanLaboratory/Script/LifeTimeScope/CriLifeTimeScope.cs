using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Audio
{
    public class CriLifeTimeScope : LifetimeScope
    {
        [SerializeField] private CriAudioSetting _criAudioSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_criAudioSettings).AsSelf();
            builder.Register<CriAudioManager>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<CriAudioManager>();
        }
    }
}
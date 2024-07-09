using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Audio
{
    public class CriLifeTimeScope : LifetimeScope
    {
        [SerializeField] private CriAudioSetting _criAudioSetting;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_criAudioSetting);
            
            builder.Register<CriAudioManager>(Lifetime.Singleton);
            builder.Register<ICriAudioService, CriAudioService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<CirView>();
            builder.RegisterEntryPoint<CriAudioManagerPresenter>();
        }
    }
}
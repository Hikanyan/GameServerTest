using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Audio
{
    public class CriLifeTimeScope : LifetimeScope
    {
        [SerializeField] private List<AudioCueSheet<CriAudioType>> cueSheets;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(cueSheets).AsSelf();
            builder.RegisterEntryPoint<CriAudioManager>();
            builder.RegisterEntryPoint<CirView>();
        }
    }
}
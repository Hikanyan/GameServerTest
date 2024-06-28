using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Audio
{
    public class CriLifeTimeScope : LifetimeScope
    {[SerializeField] private List<AudioCueSheet<CriAudioType>> _cueSheets;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cueSheets).AsSelf();
            builder.RegisterEntryPoint<CriAudioManager>();
            builder.RegisterEntryPoint<CirView>();
        }
    }
}
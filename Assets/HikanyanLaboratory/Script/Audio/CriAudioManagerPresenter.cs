using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioManagerPresenter:IInitializable
    {
        private readonly CriAudioManager _criAudioManager;

        public CriAudioManagerPresenter(CriAudioManager criAudioManager)
        {
            _criAudioManager = criAudioManager;
        }

        public void Initialize()
        {
            Debug.Log("CriAudioManagerPresenter Initialize");
        }
    }
}
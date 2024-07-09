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
    public class CriAudioManager
    {
        private readonly ICriAudioService _audioService;

        public CriAudioManager(ICriAudioService audioService)
        {
            _audioService = audioService;
        }

        public void PlaySound(string soundName)
        {
            _audioService.PlaySound(soundName);
        }

        public void StopSound(string soundName)
        {
            _audioService.StopSound(soundName);
        }
    }
}
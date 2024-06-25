using System;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using UniRx;
using Cysharp.Threading.Tasks;

namespace HikanyanLaboratory
{
    public abstract class CueSheetBase : ICriAudio
    {
        protected CriAtomExPlayer Player;
        protected float VolumeLevel = 1f;
        public Subject<float> VolumeChanged = new Subject<float>();

        protected CueSheetBase()
        {
            Player = new CriAtomExPlayer();
        }

        public virtual void Volume(float volume)
        {
            VolumeLevel = volume;
            VolumeChanged.OnNext(volume);
        }

        public virtual void Play(CueName cueName, bool is3d = false)
        {
            Player.SetVolume(VolumeLevel);
            Player.SetCue(null, cueName.ToString());
            Player.Start();
        }

        public virtual void Stop()
        {
            Player.Stop();
        }

        public virtual void Pause()
        {
            Player.Pause();
        }

        public virtual void Resume()
        {
            Player.Resume(CriAtomEx.ResumeMode.PausedPlayback);
        }

        public virtual void Dispose()
        {
            Player.Dispose();
        }
    }
}
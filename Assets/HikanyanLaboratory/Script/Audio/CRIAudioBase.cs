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
        protected readonly CriAtomExPlayer Player = new();
        protected float VolumeLevel = 1f;
        public readonly Subject<float> VolumeChanged = new Subject<float>();

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

        public virtual void Loop(bool loop)
        {
            Player.Loop(loop);
        }

        public virtual void Set3dListener(CriAtomEx3dListener listener)
        {
            Player.Set3dListener(listener);
        }

        public virtual void Dispose()
        {
            Player.Dispose();
        }
    }
}
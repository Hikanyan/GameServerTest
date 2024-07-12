using System;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public interface ICriAudioPlayerService : IDisposable
    {
        void Play(string cueName, float volume, bool isLoop);
        void Play3D(GameObject gameObject, string cueName, float volume, bool isLoop);
        void Stop(int index);
        void Pause(int index);
        void Resume(int index);
        void SetVolume(float volume);
        void SetMasterVolume(float masterVolume);
    }
}
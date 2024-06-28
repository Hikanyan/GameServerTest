using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public interface ICriAudioPlayer
    {
        void Play(string cueName, float volume = 1f, bool isLoop = false);
        void Play3D(GameObject gameObject, string cueName, float volume= 1f, bool isLoop= false);
        void Stop(int index);
        void Pause(int index);
        void Resume(int index);
        void SetVolume(float volume);
        void Dispose();
    }
}
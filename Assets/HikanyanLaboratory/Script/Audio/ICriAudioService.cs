using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public interface ICriAudioService
    {
        void PlaySound(string cueName);
        void PlaySound(int cueNum);
        void PlaySound3D(GameObject gameObject, string cueName);
        void PlaySound3D(GameObject gameObject, int cueNum);
        void StopSound(string cueName);
        void StopSound(int index);
        void PauseSound(string cueName);
        void PauseSound(int index);
        void ResumeSound(string cueName);
        void ResumeSound(int index);
        void SetVolume(float volume);
        void Dispose();
    }
}
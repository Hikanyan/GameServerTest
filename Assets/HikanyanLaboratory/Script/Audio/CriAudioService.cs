using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioService : ICriAudioService
    {
        private readonly CriAudioSetting _criAudioSetting;

        public CriAudioService(CriAudioSetting criAudioSetting)
        {
            _criAudioSetting = criAudioSetting;
        }

        public void PlaySound(string cueName)
        {
            CriAtomSource source = GetCriAtomSource(cueName);
            source.Play();
        }

        public void PlaySound(int cueNum)
        {
            // string cueName = _criAudioSetting.GetCueName(cueNum);
            // PlaySound(cueName);
        }

        public void PlaySound3D(GameObject gameObject, string cueName)
        {
            CriAtomSource source = gameObject.AddComponent<CriAtomSource>();
            source.cueName = cueName;
            source.Play();
        }

        public void PlaySound3D(GameObject gameObject, int cueNum)
        {
            // string cueName = _criAudioSetting.GetCueName(cueNum);
            // PlaySound3D(gameObject, cueName);
        }

        public void StopSound(string cueName)
        {
            CriAtomSource source = GetCriAtomSource(cueName);
            source.Stop();
        }

        public void StopSound(int index)
        {
            // string cueName = _criAudioSetting.GetCueName(index);
            // StopSound(cueName);
        }

        public void PauseSound(string cueName)
        {
            CriAtomSource source = GetCriAtomSource(cueName);
            source.Pause(true);
        }

        public void PauseSound(int index)
        {
            // string cueName = _criAudioSetting.GetCueName(index);
            // PauseSound(cueName);
        }

        public void ResumeSound(string cueName)
        {
            CriAtomSource source = GetCriAtomSource(cueName);
            //source.Resume(CriAtomSource.ResumeMode.AllPlayback);
        }

        public void ResumeSound(int index)
        {
            // string cueName = _criAudioSetting.GetCueName(index);
            // ResumeSound(cueName);
        }

        public void SetVolume(float volume)
        {
            //CriAtomExPlayback.SetVolume(volume);
        }

        public void Dispose()
        {
            // リソースの解放
        }

        private CriAtomSource GetCriAtomSource(string cueName)
        {
            // cueNameに基づいてCriAtomSourceを取得するロジック
            return null;
        }
    }
}

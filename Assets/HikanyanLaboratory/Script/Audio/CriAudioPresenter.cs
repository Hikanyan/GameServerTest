using System;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace HikanyanLaboratory
{
    /// <summary>
    /// CriAudioPresenterは、CriAudioManagerを使って音声を再生するためのクラスです。
    /// </summary>
    public class CriAudioPresenter : IStartable, IDisposable
    {
        private readonly AudioManager _audioManager;

        public CriAudioPresenter(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Start()
        {
            _audioManager.Initialize().Forget();
        }

        public void Play(CueSheet sheet, CueName cueName, bool is3d = false)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Play(cueName, is3d);
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Play(cueName, is3d);
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Play(cueName, is3d);
                    break;
            }
        }

        public void Stop(CueSheet sheet)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Stop();
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Stop();
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Stop();
                    break;
            }
        }

        public void Pause(CueSheet sheet)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Pause();
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Pause();
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Pause();
                    break;
            }
        }

        public void Resume(CueSheet sheet)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Resume();
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Resume();
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Resume();
                    break;
            }
        }

        public void Loop(CueSheet sheet, bool loop)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Loop(loop);
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Loop(loop);
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Loop(loop);
                    break;
            }
        }

        public void SetVolume(CueSheet sheet, float volume)
        {
            switch (sheet)
            {
                case CueSheet.BGM:
                    _audioManager.BGM.Volume(volume);
                    break;
                case CueSheet.SE:
                    _audioManager.SE.Volume(volume);
                    break;
                case CueSheet.ME:
                    _audioManager.ME.Volume(volume);
                    break;
            }
        }

        public void SetMasterVolume(float volume)
        {
            _audioManager.MasterVolume = volume;
        }

        public void GetMasterVolume()
        {
            _audioManager.MasterVolume = _audioManager.MasterVolume;
        }

        public void Dispose()
        {
            _audioManager.Dispose();
        }
    }
}
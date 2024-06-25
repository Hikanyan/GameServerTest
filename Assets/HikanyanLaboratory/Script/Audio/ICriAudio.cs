using CriWare;

namespace HikanyanLaboratory
{
    public interface ICriAudio
    {
        void Volume(float volume);
        void Play(CueName cueName, bool is3d = false);
        void Stop();
        void Pause();
        void Resume();
        void Loop(bool loop);
        void Dispose();

        void Set3dListener(CriAtomEx3dListener listener);
    }
}
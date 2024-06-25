namespace HikanyanLaboratory
{
    public interface ICriAudio
    {
        void Volume(float volume);
        void Play(CueName cueName, bool is3d = false);
        void Stop();
        void Pause();
        void Resume();
    }
}
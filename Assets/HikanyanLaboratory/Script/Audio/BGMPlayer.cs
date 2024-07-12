using CriWare;

namespace HikanyanLaboratory.Audio
{
    public class BGMPlayer : CriAudioPlayerService
    {
        public BGMPlayer(string cueSheetName, CriAtomEx3dSource source, CriAtomListener listener)
            : base(cueSheetName, source, listener)
        {
        }

        public void PlayBGM(string cueName, float volume = 1f, bool isLoop = true)
        {
            Play(cueName, volume, isLoop);
        }
    }
}
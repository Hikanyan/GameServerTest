using CriWare;

namespace HikanyanLaboratory.Audio
{
    public class SEPlayer : CriAudioPlayerService
    {
        public SEPlayer(string cueSheetName, CriAtomEx3dSource source, CriAtomListener listener)
            : base(cueSheetName, source, listener)
        {
        }

        public void PlaySe(string cueName, float volume = 1f, bool isLoop = false)
        {
            Play(cueName, volume, isLoop);
        }
    }
}
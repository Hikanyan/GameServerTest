using CriWare;

namespace HikanyanLaboratory.Audio
{
    public class MEPlayer : CriAudioPlayerService
    {
        public MEPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }
    }
}
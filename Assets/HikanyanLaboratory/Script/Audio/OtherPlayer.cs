using CriWare;

namespace HikanyanLaboratory.Audio
{
    public class OtherPlayer : CriAudioPlayerService
    {
        public OtherPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }
    }
}
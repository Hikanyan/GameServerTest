using CriWare;

namespace HikanyanLaboratory.Audio
{
    public class VoicePlayer : CriAudioPlayerService
    {
        public VoicePlayer(string cueSheetName, CriAtomListener listener) 
            : base(cueSheetName, listener)
        {
        }
    }
}
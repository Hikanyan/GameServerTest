using CriWare;
using NotImplementedException = System.NotImplementedException;

namespace HikanyanLaboratory.Audio
{
    public class VoicePlayer : CriAudioPlayerService
    {
        private CriPlayerData? _currentVoice;

        public VoicePlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }

        protected override void OnPlayerCreated(CriPlayerData playerData)
        {
            _currentVoice = playerData;
        }
    }
}
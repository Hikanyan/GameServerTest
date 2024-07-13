using CriWare;
using NotImplementedException = System.NotImplementedException;

namespace HikanyanLaboratory.Audio
{
    public class OtherPlayer : CriAudioPlayerService
    {
        private CriPlayerData? _currentOther;
        public OtherPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }

        protected override void OnPlayerCreated(CriPlayerData playerData)
        {
            _currentOther = playerData;
        }
    }
}
using CriWare;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace HikanyanLaboratory.Audio
{
    public class SEPlayer : CriAudioPlayerService
    {
        private CriPlayerData? _currentSe;
        public SEPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }

        protected override void OnPlayerCreated(CriPlayerData playerData)
        {
            _currentSe = playerData;
        }
    }
}
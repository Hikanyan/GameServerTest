using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class SEPlayer : CriAudioPlayerService
    {
        public SEPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
        }
    }
}
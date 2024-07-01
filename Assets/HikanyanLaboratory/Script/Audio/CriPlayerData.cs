using CriWare;

namespace HikanyanLaboratory.Audio
{
    public struct CriPlayerData
    {
        public CriAtomExPlayback Playback { get; set; }
        public CriAtomEx.CueInfo CueInfo { get; set; }

        public bool IsLoop => CueInfo.length < 0;
    }
}
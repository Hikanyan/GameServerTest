using CriWare;
using HikanyanLaboratory.Audio;

public class BGMPlayer : CriAudioPlayerService
{
    public BGMPlayer(string cueSheetName, CriAtomListener listener)
        : base(cueSheetName, listener)
    {
    }
}
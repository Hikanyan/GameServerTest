using CriWare;
using HikanyanLaboratory.Audio;

public class BGMPlayer : CriAudioPlayerService
{
    public BGMPlayer(string cueSheetName, CriAtomListener listener)
        : base(cueSheetName, listener)
    {
    }
    public override void Play(string cueName, float volume, bool isLoop)
    {
        // 既存のBGMを停止する
        base.Play(cueName, volume, isLoop);
    }
}
using System.Collections.Generic;
using CriWare;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class BGMPlayer : CriAudioPlayerService
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public BGMPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
            Observable.EveryUpdate()
                .Subscribe(_ => CheckPlayerStatus())
                .AddTo(_disposables);
        }

        protected override void PrePlayCheck(string cueName)
        {
            // BGM 再生時には既存の BGM を止める
            StopAllBGM();
        }

        private void StopAllBGM()
        {
            foreach (var cue in _playbacks.Keys)//_playbacks.Keysは再生中の音声の名前
            {
                Stop(cue);
            }
        }

        ~BGMPlayer()
        {
            _disposables.Dispose();
        }
    }
}
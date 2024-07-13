﻿using CriWare;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class MEPlayer : CriAudioPlayerService
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MEPlayer(string cueSheetName, CriAtomListener listener)
            : base(cueSheetName, listener)
        {
            Observable.EveryUpdate()
                .Subscribe(_ => CheckPlayerStatus())
                .AddTo(_disposables);
        }

        protected override void PrePlayCheck(string cueName)
        {
            // BGM 再生時には既存の BGM を止める
            StopAllME();
        }

        private void StopAllME()
        {
            foreach (var cue in _playbacks.Keys)//_playbacks.Keysは再生中の音声の名前
            {
                Stop(cue);
            }
        }

        ~MEPlayer()
        {
            _disposables.Dispose();
        }
    }
}
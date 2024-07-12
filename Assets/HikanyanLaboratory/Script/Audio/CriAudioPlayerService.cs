using System.Collections.Generic;
using CriWare;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioPlayerService : ICriAudioPlayerService
    {
        private readonly string _cueSheetName;
        private readonly List<CriAtomExPlayer> _players = new List<CriAtomExPlayer>();
        private readonly List<CriPlayerData> _data = new List<CriPlayerData>();
        private readonly CriAtomListener _listener;
        private float _volume = 1f;
        private const float MasterVolume = 1f;

        public CriAudioPlayerService(string cueSheetName, CriAtomListener listener)
        {
            _cueSheetName = cueSheetName;
            _listener = listener;
        }

        ~CriAudioPlayerService()
        {
            Dispose();
        }

        public virtual void Play(string cueName, float volume, bool isLoop)
        {
            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            if (tempAcb == null)
            {
                Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return;
            }

            CriPlayerData newAtomPlayer = new CriPlayerData();
            tempAcb.GetCueInfo(cueName, out var cueInfo);
            newAtomPlayer.CueInfo = cueInfo;

            CriAtomExPlayer player = new CriAtomExPlayer();
            player.SetCue(tempAcb, cueName);
            player.SetVolume(volume * _volume * MasterVolume);
            player.Loop(isLoop);
            Debug.Log($"Loop: {isLoop}");
            newAtomPlayer.Playback = player.Start();

            _players.Add(player);
            _data.Add(newAtomPlayer);
        }

        public virtual void Play3D(Transform transform, string cueName, float volume, bool isLoop)
        {
            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            if (tempAcb == null)
            {
                Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return;
            }

            CriPlayerData newAtomPlayer = new CriPlayerData();
            tempAcb.GetCueInfo(cueName, out var cueInfo);
            newAtomPlayer.CueInfo = cueInfo;

            CriAtomEx3dSource source = new CriAtomEx3dSource();
            source.SetPosition(transform.position.x, transform.position.y, transform.position.z);
            source.Update();

            CriAtomExPlayer player = new CriAtomExPlayer();
            player.Set3dSource(source);
            player.Set3dListener(_listener.nativeListener);
            player.SetCue(tempAcb, cueName);
            player.SetVolume(volume * _volume * MasterVolume);
            player.Loop(isLoop);
            newAtomPlayer.Playback = player.Start();

            _players.Add(player);
            _data.Add(newAtomPlayer);
        }

        public void Pause(string cueName)
        {
            foreach (var player in _players)
            {
                player.Pause();
            }
        }

        public void Resume(string cueName)
        {
            foreach (var player in _players)
            {
                player.Resume(CriAtomEx.ResumeMode.PausedPlayback);
            }
        }

        public void Stop(string cueName)
        {
            foreach (var player in _players)
            {
                player.Stop();
                player.Dispose();
            }

            _players.Clear();
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
            foreach (var player in _players)
            {
                player.SetVolume(_volume);
            }
        }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                player.Dispose();
            }

            _players.Clear();
        }
    }
}
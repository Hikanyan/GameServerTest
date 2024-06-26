﻿using System.Collections.Generic;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioPlayer : ICriAudioPlayer
    {
        private readonly string _cueSheetName;
        private readonly List<CriAtomExPlayer> _players = new List<CriAtomExPlayer>();
        private readonly List<CriPlayerData> _data = new List<CriPlayerData>();
        private readonly CriAtomEx3dSource _3dSource;
        private readonly CriAtomListener _listener;
        private float _volume = 1f;
        private float _masterVolume = 1f;

        public CriAudioPlayer(string cueSheetName, CriAtomEx3dSource source, CriAtomListener listener)
        {
            _cueSheetName = cueSheetName;
            _3dSource = source;
            _listener = listener;
        }

        ~CriAudioPlayer()
        {
            Dispose();
        }

        public void Play(string cueName, float volume, bool isLoop)
        {
            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            if (tempAcb == null)
            {
                UnityEngine.Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return;
            }

            CriPlayerData newAtomPlayer = new CriPlayerData();
            tempAcb.GetCueInfo(cueName, out var cueInfo);
            newAtomPlayer.CueInfo = cueInfo;

            CriAtomExPlayer player = new CriAtomExPlayer();
            player.SetCue(tempAcb, cueName);
            player.SetVolume(volume * _volume * _masterVolume);
            player.Loop(isLoop);
            newAtomPlayer.Playback = player.Start();

            _players.Add(player);
            _data.Add(newAtomPlayer);
        }

        public void Play3D(GameObject gameObject, string cueName, float volume, bool isLoop)
        {
            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            if (tempAcb == null)
            {
                UnityEngine.Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return;
            }

            CriPlayerData newAtomPlayer = new CriPlayerData();
            tempAcb.GetCueInfo(cueName, out var cueInfo);
            newAtomPlayer.CueInfo = cueInfo;

            CriAtomExPlayer player = new CriAtomExPlayer();
            player.Set3dSource(_3dSource);
            player.Set3dListener(_listener.nativeListener);
            player.SetCue(tempAcb, cueName);
            player.SetVolume(volume * _volume * _masterVolume);
            player.Loop(isLoop);
            newAtomPlayer.Playback = player.Start();

            _players.Add(player);
            _data.Add(newAtomPlayer);

            // 更新された位置を設定
            Vector3 position = gameObject.transform.position;
            _3dSource.SetPosition(position.x, position.y, position.z);
            _3dSource.Update();
        }

        public void Stop(int index)
        {
            if (index < 0 || index >= _data.Count) return;

            _data[index].Playback.Stop();
            _players[index].Dispose();
            _players.RemoveAt(index);
            _data.RemoveAt(index);
        }

        public void Pause(int index)
        {
            if (index < 0 || index >= _data.Count) return;

            _data[index].Playback.Pause();
        }

        public void Resume(int index)
        {
            if (index < 0 || index >= _data.Count) return;

            _data[index].Playback.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
            foreach (var player in _players)
            {
                player.SetVolume(_volume * _masterVolume);
            }
        }

        public void SetMasterVolume(float masterVolume)
        {
            _masterVolume = masterVolume;
            SetVolume(_volume);
        }
        
        public void Dispose()
        {
            foreach (var player in _players)
            {
                player.Dispose();
            }
        }
    }
}
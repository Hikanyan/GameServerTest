﻿using System;
using System.Collections;
using System.Collections.Generic;
using CriWare;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioManager : AbstractSingletonMonoBehaviour<CriAudioManager>
    {
        [SerializeField] private string _streamingAssetsPathAcf = "Chronicle Dimention"; //.acf
        [SerializeField] private List<AudioCueSheet<CriAudioType>> _cueSheets;
        private float _masterVolume = 1F;
        private const float Diff = 0.01F; //音量の変更があったかどうかの判定に使う

        private Action<float> _masterVolumeChanged;
        private readonly Dictionary<CriAudioType, Action<float>> _volumeChanged = new();

        private readonly Dictionary<CriAudioType, CriAtomExPlayer> _players = new();

        private readonly Dictionary<CriAudioType, List<CriPlayerData>> _playerData = new();

        private CriAtomExPlayer _3dSePlayer;
        private CriAtomEx3dSource _3dSource;
        private CriAtomListener _listener;

        private string _currentCueName = "";
        private CriAtomExAcb _currentAcb = null;

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                if (!(_masterVolume + Diff < value) && !(_masterVolume - Diff > value)) return;
                _masterVolumeChanged?.Invoke(value);
                _masterVolume = value;
            }
        }

        public override void OnAwake()
        {
            Initialize();
        }

        public struct CriPlayerData
        {
            public CriAtomExPlayback Playback { get; set; }
            public CriAtomEx.CueInfo CueInfo { get; set; }

            public bool IsLoop => CueInfo.length < 0;
        }

        private void Initialize()
        {
            if (_cueSheets == null || _cueSheets.Count == 0)
            {
                UnityEngine.Debug.LogError("CueSheets are not initialized in CriAudioManager");
                return;
            }

            string path = Application.streamingAssetsPath + $"/{_streamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);

            new GameObject("CriAtom").AddComponent<CriAtom>();

            foreach (var cueSheet in _cueSheets)
            {
                CriAtom.AddCueSheet(cueSheet.Name, $"{cueSheet.Name}.acb",
                    cueSheet.AwbPath != "" ? $"{cueSheet.AwbPath}.awb" : null, null);

                if (!_players.ContainsKey(cueSheet.Type))
                {
                    _players[cueSheet.Type] = new CriAtomExPlayer();
                }

                if (!_playerData.ContainsKey(cueSheet.Type))
                {
                    _playerData[cueSheet.Type] = new List<CriPlayerData>();
                }

                _volumeChanged[cueSheet.Type] = volume =>
                {
                    foreach (var data in _playerData[cueSheet.Type])
                    {
                        _players[cueSheet.Type].SetVolume(_masterVolume * volume);
                        _players[cueSheet.Type].Update(data.Playback);
                    }
                };
            }

            _3dSePlayer = new CriAtomExPlayer();
            _3dSource = new CriAtomEx3dSource();
            _3dSource.SetPosition(0, 0, 0);
            _3dSource.Update();
            _3dSePlayer.Set3dSource(_3dSource);

            _listener = FindObjectOfType<CriAtomListener>();
            if (_listener == null)
            {
                UnityEngine.Debug.LogWarning($"{nameof(CriAtomListener)} が見つかりません。");
                _listener = new GameObject(nameof(CriAtomListener)).AddComponent<CriAtomListener>();
            }
            else
            {
                foreach (var player in _players.Values)
                {
                    player.Set3dListener(_listener.nativeListener);
                }

                _3dSePlayer.Set3dListener(_listener.nativeListener);
            }

            _masterVolumeChanged += volume =>
            {
                foreach (var cueSheet in _cueSheets)
                {
                    _volumeChanged[cueSheet.Type]?.Invoke(volume);
                }
            };
        }

        public void PauseAll()
        {
            foreach (var player in _players.Values)
            {
                if (player.GetStatus() == CriAtomExPlayer.Status.Playing)
                {
                    player.Pause();
                }
            }
        }

        public void ResumeAll()
        {
            foreach (var player in _players.Values)
            {
                player.Resume(CriAtomEx.ResumeMode.PausedPlayback);
            }
        }

        /// <summary>
        /// Typeに対応するCueSheetの名前で指定されたCueを再生します。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cueName"></param>
        /// <param name="volume"></param>
        /// <param name="is3d"></param>
        public void Play(CriAudioType type, string cueName, float volume = 1f, bool is3d = false)
        {
            var cueSheet = _cueSheets.Find(sheet => sheet.Type.Equals(type));
            if (cueSheet == null)
            {
                UnityEngine.Debug.LogError($"Cue sheet {type} not found.");
                return;
            }

            if (!is3d)
            {
                is3d = IsCue3D(cueSheet.Name, cueName);
            }

            var temp = CriAtom.GetCueSheet(cueSheet.Name).acb;

            if (type.Equals(cueSheet.Type) && _currentCueName == cueName &&
                _players[type].GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                return;
            }

            Stop(type);

            if (temp == null)
            {
                UnityEngine.Debug.LogError("ACB is null. Cannot be played.");
                return;
            }

            var player = is3d ? _3dSePlayer : _players[type];

            player.SetCue(temp, cueName);
            var playback = player.Start();
            _currentAcb = temp;
            _currentCueName = cueName;

            var newPlayerData = new CriPlayerData
            {
                CueInfo = new CriAtomEx.CueInfo(),
                Playback = playback
            };
            _playerData[type].Add(newPlayerData);
        }

        /// <summary>
        /// Typeに対応するCueSheetの名前で指定されたCueを一時停止します。
        /// </summary>
        /// <param name="type"></param>
        public void Pause(CriAudioType type)
        {
            var playerDataList = _playerData[type];
            foreach (var playerData in playerDataList)
            {
                playerData.Playback.Pause();
            }
        }

        /// <summary>
        /// Typeに対応するCueSheetの名前で指定されたCueを再開します。
        /// </summary>
        /// <param name="type"></param>
        public void Resume(CriAudioType type)
        {
            var playerDataList = _playerData[type];
            foreach (var playerData in playerDataList)
            {
                playerData.Playback.Resume(CriAtomEx.ResumeMode.PausedPlayback);
            }
        }

        /// <summary>
        /// Typeに対応するCueSheetの名前で指定されたCueを停止します。
        /// </summary>
        /// <param name="type"></param>
        public void Stop(CriAudioType type)
        {
            var playerDataList = _playerData[type];
            foreach (var playerData in playerDataList)
            {
                playerData.Playback.Stop();
            }

            _playerData[type].Clear();
        }

        public void Loop(CriAudioType type, bool isLoop)
        {
            var playerDataList = _playerData[type];
            for (int i = 0; i < playerDataList.Count; i++)
            {
            }
        }
        
        private bool IsCue3D(string cueSheetName, string cueName)
        {
            var acb = CriAtom.GetCueSheet(cueSheetName).acb;
            if (acb != null)
            {
                CriAtomEx.CueInfo cueInfo;
                acb.GetCueInfo(cueName, out cueInfo);
                return cueInfo.pos3dInfo.maxAttenuationDistance > 0;
            }

            return false;
        }

        protected override bool UseDontDestroyOnLoad => true;

        public List<CriPlayerData> GetPlayerData(CriAudioType type)
        {
            if (_playerData.TryGetValue(type, out var playerDataList))
            {
                return playerDataList;
            }

            return new List<CriPlayerData>();
        }

        public List<CriAtomExPlayer> GetPlayers(CriAudioType type)
        {
            if (_players.TryGetValue(type, out var player))
            {
                return new List<CriAtomExPlayer> { player };
            }

            return new List<CriAtomExPlayer>();
        }
    }
}
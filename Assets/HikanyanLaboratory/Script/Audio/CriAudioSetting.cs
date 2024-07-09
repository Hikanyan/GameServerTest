﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    [CreateAssetMenu(fileName = "CriAudioSetting", menuName = "HikanyanLaboratory/Audio/CriAudioSetting")]
    [Serializable]
    public class CriAudioSetting : ScriptableObject
    {
        [SerializeField] private string _streamingAssetsPathAcf;
        [SerializeField] private List<AudioCueSheet<string>> _audioCueSheet;
        [SerializeField] private Dictionary<CriAudioType, List<string>> _cueSheetDictionary = new();

        public string StreamingAssetsPathAcf => _streamingAssetsPathAcf;
        public List<AudioCueSheet<string>> AudioCueSheet => _audioCueSheet;
        public Dictionary<CriAudioType, List<string>> CueSheetDictionary => _cueSheetDictionary;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _audioCueSheet ??= new List<AudioCueSheet<string>>();
            _cueSheetDictionary.Clear();
        }

        /// <summary>
        /// キューシートを検索する
        /// </summary>
        public void SearchCueSheet()
        {
            CriAudioLoader criAudioLoader = new CriAudioLoader();
            criAudioLoader.SetCriAudioSetting(this);
            criAudioLoader.SearchCueSheet();
        }

        public void SetStreamingAssetsPathAcf(string path)
        {
            _streamingAssetsPathAcf = path;
        }

        public void SetAudioCueSheet(List<AudioCueSheet<string>> cueSheets)
        {
            _audioCueSheet = cueSheets;
        }

        public void AddCueSheet(CriAudioType cueSheetType, List<string> cueNames)
        {
            if (!_cueSheetDictionary.ContainsKey(cueSheetType))
            {
                _cueSheetDictionary[cueSheetType] = cueNames;
            }
        }

        public string GetCueName(CriAudioType cueSheetType, int index)
        {
            if (_cueSheetDictionary.ContainsKey(cueSheetType) && index < _cueSheetDictionary[cueSheetType].Count)
            {
                return _cueSheetDictionary[cueSheetType][index];
            }

            return string.Empty;
        }

        /// <summary>
        /// キュー名を取得する
        /// </summary>
        /// <param name="cueSheetType"></param>
        /// <returns></returns>
        public List<string> GetCueNames(CriAudioType cueSheetType)
        {
            return _cueSheetDictionary.TryGetValue(cueSheetType, out var value) ? value : new List<string>();
        }
    }
}
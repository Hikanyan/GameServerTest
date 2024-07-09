using System;
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
        [SerializeField] private List<string> _cueNames;
        public string StreamingAssetsPathAcf => _streamingAssetsPathAcf;
        public List<AudioCueSheet<string>> AudioCueSheet => _audioCueSheet;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _audioCueSheet ??= new List<AudioCueSheet<string>>();
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


        public string GetCueName(int index)
        {
            if (index < _cueNames.Count)
            {
                return _cueNames[index];
            }

            return string.Empty;
        }
    }
}
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
    }
}
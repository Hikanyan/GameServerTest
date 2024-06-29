using System;
using System.Collections.Generic;
using System.IO;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    [CreateAssetMenu(fileName = "CriAudioSetting", menuName = "HikanyanLaboratory/Audio/CriAudioSetting")]
    [Serializable]
    public class CriAudioSetting : ScriptableObject
    {
        [SerializeField] private string _streamingAssetsPathAcf;
        [SerializeField] private List<AudioCueSheet<string>> _audioCueSheet; // enum の代わりに string を使用

        public string StreamingAssetsPathAcf => _streamingAssetsPathAcf;
        public List<AudioCueSheet<string>> AudioCueSheet => _audioCueSheet;
        
        public void Initialize()
        {
            _audioCueSheet = new List<AudioCueSheet<string>>();
        }

        public void SearchCueSheet()
        {
            
        }
    }
}
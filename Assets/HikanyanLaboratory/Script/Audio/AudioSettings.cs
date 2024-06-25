using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HikanyanLaboratory
{
    [Serializable]
    public class CueSheetPath
    {
        public CueSheet _cueSheet;
        public string _path;
    }

    [Serializable]
    public class AwbPath
    {
        public CueSheet _cueSheet;
        public string _path;
    }

    [CreateAssetMenu(fileName = "AudioSettings", menuName = "HikanyanLaboratory/AudioSettings")]
    [Serializable]
    public class AudioSettings : ScriptableObject
    {
        [FormerlySerializedAs("_streamingAssetsPathAcf")] public string streamingAssetsPathAcf = "HikanyanLaboratory";

        [FormerlySerializedAs("_cueSheetPaths")] public List<CueSheetPath> cueSheetPaths = new()
        {
            new CueSheetPath { _cueSheet = CueSheet.BGM, _path = "CueSheet_BGM" },
            new CueSheetPath { _cueSheet = CueSheet.SE, _path = "CueSheet_SE" },
            new CueSheetPath { _cueSheet = CueSheet.ME, _path = "CueSheet_ME" }
        };

        public List<AwbPath> AwbPaths = new();
    }
}
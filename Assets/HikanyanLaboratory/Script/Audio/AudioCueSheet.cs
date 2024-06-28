using System;
using UnityEngine.Serialization;

namespace HikanyanLaboratory.Audio
{
    [Serializable]
    public class AudioCueSheet<T> where T : Enum
    {
        public T Type;
        public string CueSheetName;
        public string AcfPath;
        public string AcbPath;
        public string AwbPath;
    }
}
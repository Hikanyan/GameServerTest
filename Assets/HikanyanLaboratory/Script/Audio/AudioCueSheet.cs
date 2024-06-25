using System;

namespace HikanyanLaboratory.Audio
{
    [Serializable]
    public class AudioCueSheet<T> where T : Enum
    {
        public T Type;
        public string Name;
        public string AwbPath;
    }
}
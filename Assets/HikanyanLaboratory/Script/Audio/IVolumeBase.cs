using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public interface IVolumeBase
    {
        void Initialize(string label, float initialValue);
        void SetValue(float value);
    }
}
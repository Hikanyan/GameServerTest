using UnityEngine;
using UnityEngine.Serialization;

namespace HikanyanLaboratory.Audio
{
    public class Test : MonoBehaviour
    {
        [SerializeField] CriAudioType _criAudioType;
        [SerializeField] string _cueSheetName;

        void Start()
        {
            CriAudioManager.Instance.Play(_criAudioType, _cueSheetName);
        }
    }
}
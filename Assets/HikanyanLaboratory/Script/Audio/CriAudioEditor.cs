using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory
{
    /// <summary>
    /// CRI Audio Editor デバッグ用
    /// </summary>
    public class CriAudioEditor : MonoBehaviour
    {
        [SerializeField] public CueSheet _cueSheet;
        [SerializeField] public CueName _cueName;
        [SerializeField] public bool _is3d;
        [SerializeField] public bool _loop;
        [SerializeField] public float _volume = 1f;

        private CriAudioPresenter _audioPresenter;
        private float _previousVolume;

        [Inject]
        public void Construct(CriAudioPresenter audioPresenter)
        {
            _audioPresenter = audioPresenter;
        }

        private void Awake()
        {
            if (_audioPresenter == null)
            {
                UnityEngine.Debug.LogError("CriAudioPresenter is not assigned.");
                return;
            }

            _audioPresenter.Start();
            _previousVolume = _volume;
        }

        private void OnDestroy()
        {
            _audioPresenter?.Dispose();
        }

        public void PlayAudio()
        {
            if (_audioPresenter == null) return;
            _audioPresenter.Play(_cueSheet, _cueName, _is3d);
        }

        public void StopAudio()
        {
            if (_audioPresenter == null) return;
            _audioPresenter.Stop(_cueSheet);
        }

        public void PauseAudio()
        {
            if (_audioPresenter == null) return;
            _audioPresenter.Pause(_cueSheet);
        }

        public void ResumeAudio()
        {
            if (_audioPresenter == null) return;
            _audioPresenter.Resume(_cueSheet);
        }

        private void OnValidate()
        {
            if (_audioPresenter != null && !Mathf.Approximately(_volume, _previousVolume))
            {
                _audioPresenter.SetVolume(_cueSheet, _volume);
                _previousVolume = _volume;
            }
        }
    }
}
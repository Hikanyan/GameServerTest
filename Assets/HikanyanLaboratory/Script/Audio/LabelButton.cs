using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Audio
{
    [Serializable]
    public class LabelButton : MonoBehaviour
    {
        [SerializeField] private string _label;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Toggle _loopToggle;

        private CueNameControl _cueNameControl;
        private CriAudioManager _criAudioManager;
        private CriAudioType _audioType;
        private string _cueName;

        // public void Construct(CriAudioManager criAudioManager)
        // {
        //     _criAudioManager = criAudioManager;
        // }

        public void Initialize(string label, CriAudioType audioType, CueNameControl cueNameControl)
        {
            //_criAudioManager = CriAudioManager.Instance;
            _label = label;
            _audioType = audioType;
            _cueNameControl = cueNameControl;

            _playButton.onClick.AddListener(Play);
            _pauseButton.onClick.AddListener(Pause);
            _resumeButton.onClick.AddListener(Resume);
            _stopButton.onClick.AddListener(Stop);
            _loopToggle.onValueChanged.AddListener(Loop);
        }

        public void Play()
        {
            _cueName = _cueNameControl.GetCueName();
            UnityEngine.Debug.Log(_cueName);
            if (!string.IsNullOrEmpty(_cueName))
            {
                //_criAudioManager.Play(_audioType, _cueName);
            }
        }

        public void Pause()
        {
            //_criAudioManager.Pause(_audioType);
        }

        public void Resume()
        {
            //_criAudioManager.Resume(_audioType);
        }

        public void Stop()
        {
            //_criAudioManager.Stop(_audioType);
        }
        public void Loop(bool isLoop)
        {
            //_criAudioManager.Loop(_audioType, isLoop);
        }
    }
}
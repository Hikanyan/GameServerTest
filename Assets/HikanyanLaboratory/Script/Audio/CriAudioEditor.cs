using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory
{
    /// <summary>
    /// CRI Audio Editor デバッグ用
    /// </summary>
    public class CriAudioEditor : MonoBehaviour
    {
        // 再生したいキューシートを指定
        [SerializeField] private CueSheet _cueSheet;

        // 再生したいキュー名を指定
        [SerializeField] private CueName _cueName;

        // 3Dサウンドを使用するかどうか ボタン
        [SerializeField] private bool _is3d;

        // 再生するかどうか ボタン
        [SerializeField] private bool _play;

        // 停止するかどうか ボタン
        [SerializeField] private bool _stop;

        // 一時停止するかどうか ボタン
        [SerializeField] private bool _pause;

        // 再開するかどうか ボタン
        [SerializeField] private bool _resume;

        // ボリュームを指定 スライダー
        [SerializeField] private float _volume = 1f;


        private CriAudioPresenter _audioPresenter;
        private float _previousVolume;

        [Inject]
        public void Construct(CriAudioPresenter audioPresenter)
        {
            _audioPresenter = audioPresenter;
        }

        private void Awake()
        {
            _audioPresenter.Start();
            _previousVolume = _volume;
        }

        private void OnDestroy()
        {
            _audioPresenter?.Dispose();
        }

        private void OnGUI()
        {
            GUILayout.Label("CRI Audio Editor", EditorStyles.boldLabel);

            _cueSheet = (CueSheet)EditorGUILayout.EnumPopup("Cue Sheet", _cueSheet);
            _cueName = (CueName)EditorGUILayout.EnumPopup("Cue Name", _cueName);
            _is3d = EditorGUILayout.Toggle("Use 3D Sound", _is3d);

            _volume = EditorGUILayout.Slider("Volume", _volume, 0f, 1f);
            if (!Mathf.Approximately(_volume, _previousVolume))
            {
                _audioPresenter.SetVolume(_cueSheet, _volume);
                _previousVolume = _volume;
            }

            if (GUILayout.Button("Play"))
            {
                _audioPresenter.Play(_cueSheet, _cueName, _is3d);
            }

            if (GUILayout.Button("Stop"))
            {
                _audioPresenter.Stop(_cueSheet);
            }

            if (GUILayout.Button("Pause"))
            {
                _audioPresenter.Pause(_cueSheet);
            }

            if (GUILayout.Button("Resume"))
            {
                _audioPresenter.Resume(_cueSheet);
            }
        }
    }
}
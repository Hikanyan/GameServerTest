using System.Collections;
using System.Collections.Generic;
using HikanyanLaboratory.System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace HikanyanLaboratory.Debug
{
    public class DebugClass : MonoBehaviour
    {
        [Header("設定値")] 
        [SerializeField, Header("デバックモードを付けるか")] private bool _isDebugMode;

        [SerializeField, Header("FPSを表示するか")] private bool _isShowFPS = true;

        [SerializeField, Header("設定フレームレート")] private int _targetFrameRate = 60;

        [SerializeField, Header("FPS警告表示の値")] private float _fpsThreshold1 = 30f; // この値を下回ると黄色

        [SerializeField, Header("FPSエラー表示の値")] private float _fpsThreshold2 = 15f; // この値を下回ると赤色

        [SerializeField, Header("VsyncをOFFにするか")] private bool _isVsyncOff = false;

        [SerializeField, Header("ログの表示時間")] private float _displayTime = 5f; // テキストが表示される時間（秒）

        [SerializeField, Header("ログの最大表示行数")] private int _maxLines = 5; // 表示する最大行数

        [Header("参照用")] 
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _logText;

        private DebugManager _debugManager;

        [Inject]
        public void Construct(DebugManager debugManager)
        {
            _debugManager = debugManager;
        }

        private void Awake()
        {
            if (!_isDebugMode)
            {
                Destroy(this.gameObject);
                return;
            }

            _debugManager.SetUIElements(_fpsText, _timerText, _logText);
            _debugManager.SetSettings(_isShowFPS, _targetFrameRate, _fpsThreshold1, _fpsThreshold2, _isVsyncOff,
                _displayTime, _maxLines);
            _debugManager.Initialize();
        }

        private void Update()
        {
            if (!_isDebugMode) return;
            _debugManager.Update();
        }
    }
}
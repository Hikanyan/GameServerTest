using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Debug
{
    public class DebugManager
    {
        private Text _fpsText;
        private Text _timerText;
        private Text _logText;

        private bool _isShowFPS;
        private int _targetFrameRate;
        private float _fpsThreshold1;
        private float _fpsThreshold2;
        private bool _isVsyncOff;
        private float _displayTime;
        private int _maxLines;

        private float _startTime;
        private float _deltaTime;
        private Queue<string> _textLines = new Queue<string>();

        public void SetUIElements(Text fpsText, Text timerText, Text logText)
        {
            _fpsText = fpsText;
            _timerText = timerText;
            _logText = logText;
        }

        public void SetSettings(bool isShowFPS, int targetFrameRate, float fpsThreshold1, float fpsThreshold2, bool isVsyncOff, float displayTime, int maxLines)
        {
            _isShowFPS = isShowFPS;
            _targetFrameRate = targetFrameRate;
            _fpsThreshold1 = fpsThreshold1;
            _fpsThreshold2 = fpsThreshold2;
            _isVsyncOff = isVsyncOff;
            _displayTime = displayTime;
            _maxLines = maxLines;
        }

        public void Initialize()
        {
            SetFPSSetting();
            _startTime = Time.time;
        }

        public void Update()
        {
            ShowFPS();
            ShowTime();
        }

        #region FPS

        private void SetFPSSetting()
        {
            QualitySettings.vSyncCount = _isVsyncOff ? 0 : 1;
            Application.targetFrameRate = _targetFrameRate;
        }

        private void ShowFPS()
        {
            if (!_isShowFPS || _fpsText == null) return;

            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;

            // FPSに応じた色の選択
            string color = "green"; // デフォルトは白色
            if (fps < _fpsThreshold2)
            {
                color = "red";
            }
            else if (fps < _fpsThreshold1)
            {
                color = "yellow";
            }

            // リッチテキストを使用して色を適用
            _fpsText.text = $"<color={color}>FPS: {fps:0.}</color>";
        }

        #endregion

        #region タイマー

        private void ShowTime()
        {
            if (_timerText == null) return;
            float t = Time.time - _startTime; // 開始からの経過時間を計算

            int hours = (int)t / 3600;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            _timerText.text = string.Format("PlayTime: {0:D2}:{1:D2}:{2:00.00}", hours, minutes, seconds);
        }

        #endregion

        #region ログ

        private void AddText(string text, Color logColor)
        {
            // 色情報をリッチテキスト形式でテキストに追加
            string coloredText = $"<color=#{ColorUtility.ToHtmlStringRGB(logColor)}>{text}</color>";

            if (_textLines.Count >= _maxLines)
            {
                _textLines.Dequeue(); // 最大行数に達したら、一番古い行を削除
            }

            _textLines.Enqueue(coloredText); // 新しいテキスト行を追加
            UpdateTextDisplay();
            UniTask.Void(async () =>
            {
                await UniTask.Delay((int)(_displayTime * 1000));
                RemoveText();
            });
        }

        // テキスト表示を更新するメソッド
        private void UpdateTextDisplay()
        {
            if (_logText == null) return;
            _logText.text = string.Join("\n", _textLines.ToArray()); // キュー内の全テキスト行を改行で結合して表示
        }

        private void RemoveText()
        {
            if (_textLines.Count > 0)
            {
                _textLines.Dequeue(); // 一番古いテキスト行を削除
                UpdateTextDisplay();
            }
        }

        public void ShowErrorLog(string message)
        {
            AddText(message, Color.red);
        }

        public void ShowWarningLog(string message)
        {
            AddText(message, Color.yellow);
        }

        public void ShowLog(string message)
        {
            AddText(message, Color.green); // または、デフォルトのテキスト色を使いたい場合はこの引数を省略してもよい
        }

        #endregion
    }
}

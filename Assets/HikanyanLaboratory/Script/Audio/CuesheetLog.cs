using System.IO;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CuesheetLog : MonoBehaviour
    {
        private string filePath;

        public CuesheetLog(string filePath)
        {
            this.filePath = filePath;
            InitializeLogFile();
        }

        private void InitializeLogFile()
        {
            // 初期化のためにファイルを空にする
            File.WriteAllText(filePath, "CueSheet Log\n");
        }

        public void LogCueSheet(string cueSheetPath, string cueName)
        {
            string logEntry = $"CueSheet: {cueSheetPath}, CueName: {cueName}\n";
            File.AppendAllText(filePath, logEntry);
        }

        public void DisplayLog()
        {
            string logContent = File.ReadAllText(filePath);
            UnityEngine.Debug.Log(logContent);
        }
    }
}
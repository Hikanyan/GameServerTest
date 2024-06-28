using System.IO;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class TestCuesheetLog : MonoBehaviour
    {
        private CuesheetLog cuesheetLog;

        void Start()
        {
            // ファイルパスを指定してCuesheetLogクラスを初期化
            string filePath = Path.Combine(Application.persistentDataPath, "CuesheetLog.txt");
            cuesheetLog = new CuesheetLog(filePath);

            // キューシートのログを追加
            cuesheetLog.LogCueSheet("path/to/cuesheet1.acb", "CueName1");
            cuesheetLog.LogCueSheet("path/to/cuesheet2.acb", "CueName2");

            // ログの内容を表示
            cuesheetLog.DisplayLog();
        }
    }
}
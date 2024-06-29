using System.Collections.Generic;
using System.IO;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioLoader : MonoBehaviour
    {
        private CriAudioSetting _audioSetting;
        private HashSet<string> enumEntries;

        public void SetCriAudioSetting(CriAudioSetting audioSetting)
        {
            _audioSetting = audioSetting;
        }

        private void Awake()
        {
            if (_audioSetting == null)
            {
                UnityEngine.Debug.LogError("CriAudioSettingが設定されていません。");
                return;
            }

            Initialize();
            SearchCueSheet();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            string path = Application.streamingAssetsPath + $"/{_audioSetting.StreamingAssetsPathAcf}.acf";
            enumEntries = new HashSet<string>();
            CriAtomEx.RegisterAcf(null, path);
        }

        /// <summary>
        /// キューシートの検索
        /// </summary>
        public void SearchCueSheet()
        {
            // キューシートの検索と設定
            string searchPath = Application.streamingAssetsPath; // 検索パスを指定
            string[] acbFiles = Directory.GetFiles(searchPath, "*.acb", SearchOption.AllDirectories);

            foreach (string acbFile in acbFiles)
            {
                string acbPath = acbFile.Replace("\\", "/");
                string awbPath = acbPath.Replace(".acb", ".awb");
                if (!File.Exists(awbPath))
                {
                    awbPath = string.Empty; // AWBファイルが見つからなかった場合は空白を設定
                }

                CriAtomExAcb acb = CriAtomExAcb.LoadAcbFile(null, acbPath, awbPath);

                if (acb != null)
                {
                    string cueSheetName = Path.GetFileNameWithoutExtension(acbPath);
                    string acbName = Path.GetFileNameWithoutExtension(acbPath);
                    string awbName = Path.GetFileNameWithoutExtension(awbPath);
                    var audioCueSheet = new AudioCueSheet<string>
                    {
                        Type = cueSheetName, // string を使用
                        CueSheetName = cueSheetName,
                        AcfPath = _audioSetting.StreamingAssetsPathAcf,
                        AcbPath = acbName,
                        AwbPath = awbName
                    };
                    _audioSetting.AudioCueSheet.Add(audioCueSheet);
                    enumEntries.Add(cueSheetName); // 新しい enum エントリを追加
                    acb.Dispose();
                }
            }
        }

        /// <summary>
        /// キューシートの表示
        /// </summary>
        public void DisplayCueSheets()
        {
            foreach (var cueSheet in _audioSetting.AudioCueSheet)
            {
                UnityEngine.Debug.Log(
                    $"Type: {cueSheet.Type}, CueSheetName: {cueSheet.CueSheetName}, AcfPath: {cueSheet.AcfPath}, AcbPath: {cueSheet.AcbPath}, AwbPath: {cueSheet.AwbPath}");
            }
        }

        public List<AudioCueSheet<string>> GetCueSheets()
        {
            return _audioSetting.AudioCueSheet;
        }

        /// <summary>
        /// CriAudioType enum を生成
        /// </summary>
        public void GenerateEnumFile()
        {
            string directoryPath = Path.Combine(Application.dataPath, "HikanyanLaboratory/Script/Editor");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "GeneratedCriAudioTypeEnum.cs");
            using (StreamWriter writer = new StreamWriter(filePath, false)) // false を設定して上書き
            {
                writer.WriteLine(CriAudioTypeFile());
            }

            UnityEngine.Debug.Log(
                $"GeneratedCriAudioTypeEnum.cs has been generated at {filePath}. Please recompile the project.");
        }

        /// <summary>
        /// CriAudioType enum のテキストを生成
        /// </summary>
        /// <returns></returns>
        private string CriAudioTypeFile()
        {
            string text =
                "namespace HikanyanLaboratory.Audio\n" +
                "{\n" +
                "    public enum CriAudioType\n" +
                "    {\n" +
                "        Master,";

            foreach (var entry in enumEntries)
            {
                text += $"\n        {entry},";
            }

            text += "\n" +
                    "       Other\n" +
                    "    }\n" +
                    "}";

            return text;
        }
    }
}
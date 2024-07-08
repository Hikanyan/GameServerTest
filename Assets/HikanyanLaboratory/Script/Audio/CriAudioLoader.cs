using System.Collections.Generic;
using System.IO;
using System.Linq;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioLoader
    {
        private CriAudioSetting _audioSetting;
        private HashSet<string> _enumEntries;

        public void SetCriAudioSetting(CriAudioSetting audioSetting)
        {
            if (audioSetting == null)
            {
                UnityEngine.Debug.LogError("CriAudioSetting is null.");
                return;
            }

            _audioSetting = audioSetting;
        }

        public void Initialize()
        {
            if (_audioSetting == null)
            {
                UnityEngine.Debug.LogError("CriAudioSetting is not set.");
                return;
            }

            string path = Application.streamingAssetsPath + $"/{_audioSetting.StreamingAssetsPathAcf}.acf";
            _enumEntries = new HashSet<string>();
            CriAtomEx.RegisterAcf(null, path);
        }

        public void SearchCueSheet()
        {
            if (_audioSetting == null)
            {
                UnityEngine.Debug.LogError("CriAudioSetting が設定されていません。\n" +
                                           "CriAudioSetting を設定してから呼び出してください。");
                return;
            }

            if (_audioSetting.AudioCueSheet == null)
            {
                UnityEngine.Debug.LogError("AudioCueSheet が null です。\n" +
                                           "AudioCueSheet を初期化してから呼び出してください。");
                return;
            }

            // ACF ファイルを検索して設定
            string searchPath = Application.streamingAssetsPath;
            string acfFilePath = Directory.GetFiles(searchPath, "*.acf", SearchOption.AllDirectories).FirstOrDefault();
            if (acfFilePath != null)
            {
                string acfFileName = Path.GetFileNameWithoutExtension(acfFilePath); // ファイル名のみ取得
                _audioSetting.SetStreamingAssetsPathAcf(acfFileName);
            }
            else
            {
                UnityEngine.Debug.LogError("No ACF file found in StreamingAssets.");
                return;
            }

            // キューシートリストをクリア
            _audioSetting.AudioCueSheet.Clear();


            string[] acbFiles = Directory.GetFiles(searchPath, "*.acb", SearchOption.AllDirectories);

            foreach (string acbFile in acbFiles)
            {
                string acbPath = acbFile.Replace("\\", "/");
                string awbPath = acbPath.Replace(".acb", ".awb");
                if (!File.Exists(awbPath))
                {
                    awbPath = string.Empty;
                }

                CriAtomExAcb acb = CriAtomExAcb.LoadAcbFile(null, acbPath, awbPath);

                if (acb != null)
                {
                    string cueSheetName = Path.GetFileNameWithoutExtension(acbPath);
                    string acbName = Path.GetFileNameWithoutExtension(acbPath);
                    string awbName = Path.GetFileNameWithoutExtension(awbPath);
                    var audioCueSheet = new AudioCueSheet<string>
                    {
                        Type = cueSheetName,
                        CueSheetName = cueSheetName,
                        AcbPath = acbName,
                        AwbPath = awbName
                    };
                    _audioSetting.AudioCueSheet.Add(audioCueSheet);
                    _enumEntries.Add(cueSheetName);
                    acb.Dispose();
                }
            }
        }

        private HashSet<string> LoadExistingEnumEntries(string filePath)
        {
            var existingEntries = new HashSet<string>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("{") || line.Trim().StartsWith("namespace") || line.Trim().StartsWith("public enum CriAudioType"))
                    {
                        continue;
                    }

                    if (line.Trim().StartsWith("}"))
                    {
                        break;
                    }

                    var entry = line.Trim().TrimEnd(',').Trim();
                    if (!string.IsNullOrEmpty(entry) && entry != "Master" && entry != "Other")
                    {
                        existingEntries.Add(entry);
                    }
                }
            }

            return existingEntries;
        }

        public void GenerateEnumFile()
        {
            string directoryPath = Path.Combine(Application.dataPath, "HikanyanLaboratory/Script/Audio");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "CriAudioType.cs");
            var existingEntries = LoadExistingEnumEntries(filePath);

            _enumEntries.UnionWith(existingEntries);

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(CriAudioTypeFile());
            }

            UnityEngine.Debug.Log(
                $"GeneratedCriAudioTypeEnum.cs has been generated at {filePath}. Please recompile the project.");
        }

        private string CriAudioTypeFile()
        {
            string text =
                "namespace HikanyanLaboratory.Audio\n" +
                "{\n" +
                "    public enum CriAudioType\n" +
                "    {\n" +
                "        Master,";

            foreach (var entry in _enumEntries)
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
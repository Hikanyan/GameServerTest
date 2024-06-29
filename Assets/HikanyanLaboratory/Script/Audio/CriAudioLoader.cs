using System.Collections.Generic;
using System.IO;
using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class CriAudioLoader
    {
        private CriAudioSetting _audioSetting;
        private HashSet<string> enumEntries;

        public void SetCriAudioSetting(CriAudioSetting audioSetting)
        {
            _audioSetting = audioSetting;
        }

        public void Initialize()
        {
            string path = Application.streamingAssetsPath + $"/{_audioSetting.StreamingAssetsPathAcf}.acf";
            enumEntries = new HashSet<string>();
            CriAtomEx.RegisterAcf(null, path);
        }

        public void SearchCueSheet()
        {
            string searchPath = Application.streamingAssetsPath;
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
                        AcfPath = _audioSetting.StreamingAssetsPathAcf,
                        AcbPath = acbName,
                        AwbPath = awbName
                    };
                    _audioSetting.AudioCueSheet.Add(audioCueSheet);
                    enumEntries.Add(cueSheetName);
                    acb.Dispose();
                }
            }
        }

        public void DisplayCueSheets()
        {
            foreach (var cueSheet in _audioSetting.AudioCueSheet)
            {
                UnityEngine. Debug.Log(
                    $"Type: {cueSheet.Type}, CueSheetName: {cueSheet.CueSheetName}, AcfPath: {cueSheet.AcfPath}, AcbPath: {cueSheet.AcbPath}, AwbPath: {cueSheet.AwbPath}");
            }
        }

        public List<AudioCueSheet<string>> GetCueSheets()
        {
            return _audioSetting.AudioCueSheet;
        }

        public void GenerateEnumFile()
        {
            string directoryPath = Path.Combine(Application.dataPath, "HikanyanLaboratory/Script/Audio");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "CriAudioType.cs");
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
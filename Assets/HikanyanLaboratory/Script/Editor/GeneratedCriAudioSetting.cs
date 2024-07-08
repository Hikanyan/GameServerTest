using HikanyanLaboratory.Audio;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Hikanyan.Core
{
    public class GeneratedCriAudioSetting
    {
        [MenuItem("HikanyanTools/CriAudio/CriAudioSetting")]
        public static void CreateOrUpdateCriAudioSetting()
        {
            string assetPath = "Assets/HikanyanLaboratory/GameData/ScriptableObject/Cri/CriAudioSetting.asset";

            // ScriptableObjectのインスタンスをロードまたは新規作成
            CriAudioSetting criAudioSetting = AssetDatabase.LoadAssetAtPath<CriAudioSetting>(assetPath);
            if (criAudioSetting == null)
            {
                criAudioSetting = ScriptableObject.CreateInstance<CriAudioSetting>();
                AssetDatabase.CreateAsset(criAudioSetting, assetPath);
                Debug.Log("CriAudioSetting asset が作成されました。");
            }
            else
            {
                Debug.Log("CriAudioSetting asset は既に存在します。");
            }

            // 既存のAudioCueSheetエントリを保持
            var existingCueSheets = new List<AudioCueSheet<string>>(criAudioSetting.AudioCueSheet);

            // CriAudioLoaderのインスタンスを作成してキューシートの情報を取得
            CriAudioLoader criAudioLoader = new CriAudioLoader();
            criAudioLoader.SetCriAudioSetting(criAudioSetting);
            criAudioLoader.Initialize();
            criAudioLoader.SearchCueSheet();

            // 新しいキューシート情報を取得
            var newCueSheets = new List<AudioCueSheet<string>>(criAudioSetting.AudioCueSheet);

            // 既存の手動追加エントリを復元
            foreach (var existingCueSheet in existingCueSheets)
            {
                if (!newCueSheets.Exists(cs => cs.CueSheetName == existingCueSheet.CueSheetName))
                {
                    newCueSheets.Add(existingCueSheet);
                }
            }

            // 更新されたキューシート情報をCriAudioSettingに設定
            criAudioSetting.SetAudioCueSheet(newCueSheets);

            // 変更を保存
            EditorUtility.SetDirty(criAudioSetting);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("CriAudioSetting asset が更新されました。");
        }
    }
}

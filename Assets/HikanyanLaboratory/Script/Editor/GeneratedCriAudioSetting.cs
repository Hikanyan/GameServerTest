using HikanyanLaboratory.Audio;
using UnityEditor;
using UnityEngine;

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

            // CriAudioLoaderのインスタンスを作成してキューシートの情報を取得
            CriAudioLoader criAudioLoader = new CriAudioLoader();
            criAudioLoader.SetCriAudioSetting(criAudioSetting);
            criAudioLoader.Initialize();
            criAudioLoader.SearchCueSheet();

            // 取得したキューシート情報をCriAudioSettingに設定
            criAudioSetting.Initialize();
            criAudioSetting.SetAudioCueSheet(criAudioLoader.GetCueSheets());

            // 変更を保存
            EditorUtility.SetDirty(criAudioSetting);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("CriAudioSetting asset が更新されました。");
        }
    }
}
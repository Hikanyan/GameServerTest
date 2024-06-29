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
                Debug.Log("CriAudioSetting asset created.");
            }
            else
            {
                Debug.Log("CriAudioSetting asset found and will be updated.");
            }

            // CriAudioLoaderのインスタンスを作成してキューシートの情報を取得
            CriAudioLoader criAudioLoader = new CriAudioLoader();
            criAudioLoader.SetCriAudioSetting(criAudioSetting);
            criAudioLoader.Initialize();
            criAudioLoader.SearchCueSheet();

            // 取得したキューシート情報をCriAudioSettingに設定
            criAudioSetting.Initialize();
            criAudioSetting.SearchCueSheet();
            criAudioLoader.GenerateEnumFile();

            // 変更を保存
            EditorUtility.SetDirty(criAudioSetting);
            AssetDatabase.SaveAssets();

            Debug.Log("CriAudioSetting has been created or updated with ACF and CueSheet information.");
        }
    }
}
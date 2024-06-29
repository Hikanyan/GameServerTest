using HikanyanLaboratory.Audio;
using UnityEditor;
using UnityEngine;

namespace Hikanyan.Core
{
    public class GeneratedCriAudioTypeEnum
    {
        [MenuItem("HikanyanTools/CriAudio/Generate CriAudioType Enum")]
        public static void GenerateCriAudioType()
        {
            // ScriptableObjectのインスタンスをロード
            string assetPath = "Assets/HikanyanLaboratory/GameData/ScriptableObject/Cri/CriAudioSetting.asset";
            
            CriAudioSetting criAudioSetting = AssetDatabase.LoadAssetAtPath<CriAudioSetting>(assetPath);

            if (criAudioSetting != null)
            {
                CriAudioLoader criAudioLoader = new GameObject("CriAudioLoader").AddComponent<CriAudioLoader>();
                criAudioLoader.SetCriAudioSetting(criAudioSetting);
                
                criAudioLoader.Initialize();
                criAudioLoader.SearchCueSheet();
                criAudioLoader.GenerateEnumFile();
                Debug.Log("CriAudioType enum has been generated.");
                
                Object.DestroyImmediate(criAudioLoader.gameObject); // クリエートしたGameObjectを破棄
            }
            else
            {
                Debug.LogError("CriAudioSetting asset not found at " + assetPath);
            }
        }
    }
}
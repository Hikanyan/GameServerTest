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
                criAudioSetting.Initialize();
                criAudioSetting.SearchCueSheet();
                Debug.Log("CriAudioType enum has been generated.");
            }
            else
            {
                criAudioSetting = ScriptableObject.CreateInstance<CriAudioSetting>();
                Debug.LogError("CriAudioSetting asset not found at " + assetPath);
            }
        }
    }
}
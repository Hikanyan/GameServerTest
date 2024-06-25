using HikanyanLaboratory;
using UnityEditor;
using UnityEngine;

namespace Hikanyan.Core
{
    public class CriAudioEditorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            CriAudioEditor criAudioEditor = (CriAudioEditor)target;

            EditorGUILayout.LabelField("CRI Audio Editor", EditorStyles.boldLabel);

            criAudioEditor._cueSheet = (CueSheet)EditorGUILayout.EnumPopup("Cue Sheet", criAudioEditor._cueSheet);
            criAudioEditor._cueName = (CueName)EditorGUILayout.EnumPopup("Cue Name", criAudioEditor._cueName);
            criAudioEditor._is3d = EditorGUILayout.Toggle("Use 3D Sound", criAudioEditor._is3d);

            criAudioEditor._volume = EditorGUILayout.Slider("Volume", criAudioEditor._volume, 0f, 1f);
            if (GUILayout.Button("Play"))
            {
                criAudioEditor.PlayAudio();
            }

            if (GUILayout.Button("Stop"))
            {
                criAudioEditor.StopAudio();
            }

            if (GUILayout.Button("Pause"))
            {
                criAudioEditor.PauseAudio();
            }

            if (GUILayout.Button("Resume"))
            {
                criAudioEditor.ResumeAudio();
            }

            criAudioEditor._loop = EditorGUILayout.Toggle("Loop", criAudioEditor._loop);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
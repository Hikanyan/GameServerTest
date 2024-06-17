using UnityEditor;

namespace Hikanyan.Core
{
    public class BuildScript
    {
        [MenuItem("Build/Build Linux Server")]
        public static void BuildLinuxServer()
        {
            // プロジェクト名: PhotonUnityPlayFabServer
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/Main.unity" },
                locationPathName = "Server.x86_64",
                target = BuildTarget.StandaloneLinux64,
                options = BuildOptions.EnableHeadlessMode
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}
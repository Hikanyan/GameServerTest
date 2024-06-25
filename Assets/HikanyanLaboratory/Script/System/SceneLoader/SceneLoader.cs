using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.System
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            // シーンが読み込まれていない場合は読み込む
            if (!IsSceneLoaded(sceneName))
            {
                var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                while (!loadSceneOperation.isDone)
                {
                    await UniTask.Yield();
                }
            }
        }

        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (IsSceneLoaded(sceneName))
            {
                var unloadSceneOperation = SceneManager.UnloadSceneAsync(sceneName);
                while (!unloadSceneOperation.isDone)
                {
                    await UniTask.Yield();
                }
            }
        }


        private bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

namespace HikanyanLaboratory.System
{
    public class ManagerSceneController
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Scene _currentScene;

        [Inject]
        public ManagerSceneController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTaskVoid ChangeScene(string sceneName)
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync(sceneName);
        }

        public async UniTaskVoid LoadTitleScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("TitleScene");
        }

        public async UniTaskVoid LoadGameScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("InGameScene");
        }

        public async UniTaskVoid LoadResultScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("ResultScene");
        }


        private async UniTask CurrentSceneUnload()
        {
            await _sceneLoader.UnloadSceneAsync(_currentScene.name);
        }
    }
}
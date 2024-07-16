using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

namespace HikanyanLaboratory.System
{
    public class ManagerSceneController
    {
        private Scene _currentScene;
        private readonly SceneLoader _sceneLoader;
        private readonly string _managerScene = "ManagerScene";

        /// <summary>
        /// 最初にManagerSceneを読み込む
        /// </summary>
        /// <param name="sceneLoader"></param>
        [Inject]
        public ManagerSceneController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            LoadManagerScene().Forget();
        }

        /// <summary>
        /// 現在のシーンをUnloadしてから新しいシーンをLoadする
        /// </summary>
        /// <param name="sceneName"> 新しいシーンの名前 </param>
        public async UniTaskVoid ChangeScene(string sceneName)
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync(sceneName);
            _currentScene.name = sceneName;
        }

        /// <summary>
        /// TitleSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadTitleScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("TitleScene");
            _currentScene.name = "TitleScene";
        }

        /// <summary>
        /// GameSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadGameScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("InGameScene");
            _currentScene.name = "InGameScene";
        }

        /// <summary>
        /// ResultSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadResultScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("ResultScene");
            _currentScene.name = "ResultScene";
        }

        /// <summary>
        /// LobbySceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadLobbyScene()
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync("LobbyScene");
            _currentScene.name = "LobbyScene";
        }

        /// <summary>
        /// UnloadしないでManagerSceneを読み込む
        /// </summary>
        private async UniTaskVoid LoadManagerScene()
        {
            await _sceneLoader.LoadSceneAsync(_managerScene);
        }

        /// <summary>
        ///　現在のシーンをUnloadする
        /// </summary>
        private async UniTask CurrentSceneUnload()
        {
            await _sceneLoader.UnloadSceneAsync(_currentScene.name);
        }
    }
}
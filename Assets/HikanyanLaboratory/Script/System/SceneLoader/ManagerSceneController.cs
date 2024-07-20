using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.System
{
    public class ManagerSceneController
    {
        private Scene _lastScene;
        private readonly SceneLoader _sceneLoader;
        private readonly Scene _neverUnloadScene;

        /// <summary>
        /// 最初にManagerSceneを読み込む
        /// </summary>
        /// <param name="sceneLoader"></param>
        public ManagerSceneController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            // 現在のSceneを取得
            _neverUnloadScene = SceneManager.GetActiveScene();
            _lastScene = _neverUnloadScene;
            LoadManagerScene().Forget();
        }

        /// <summary>
        /// 現在のシーンをUnloadしてから新しいシーンをLoadする
        /// </summary>
        /// <param name="sceneName"> 新しいシーンの名前 </param>
        public async UniTask ChangeScene(string sceneName)
        {
            await CurrentSceneUnload();
            await _sceneLoader.LoadSceneAsync(sceneName);

            // シーンのロード完了を待つ
            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            while (!loadedScene.isLoaded)
            {
                await UniTask.Yield();
            }

            SceneManager.SetActiveScene(loadedScene);
            _lastScene = loadedScene;
        }

        /// <summary>
        /// TitleSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadTitleScene()
        {
            await ChangeScene("TitleScene");
        }

        /// <summary>
        /// GameSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadGameScene()
        {
            await ChangeScene("InGameScene");
        }

        /// <summary>
        /// ResultSceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadResultScene()
        {
            await ChangeScene("ResultScene");
        }

        /// <summary>
        /// LobbySceneを読み込む
        /// </summary>
        public async UniTaskVoid LoadLobbyScene()
        {
            await ChangeScene("LobbyScene");
        }

        /// <summary>
        /// UnloadしないでManagerSceneを読み込む
        /// </summary>
        private async UniTaskVoid LoadManagerScene()
        {
            await _sceneLoader.LoadSceneAsync(_neverUnloadScene.name, LoadSceneMode.Additive);
        }

        /// <summary>
        /// 現在のシーンをUnloadする
        /// </summary>
        private async UniTask CurrentSceneUnload()
        {
            if (_lastScene != _neverUnloadScene)
            {
                await _sceneLoader.UnloadSceneAsync(_lastScene.name);
            }
        }
    }
}
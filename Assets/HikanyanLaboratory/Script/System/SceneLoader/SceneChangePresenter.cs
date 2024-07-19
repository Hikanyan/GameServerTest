using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace HikanyanLaboratory.System
{
    public class SceneChangePresenter
    {
        private readonly SceneChangeView _view;
        private readonly ManagerSceneController _managerSceneController;
        private AsyncOperation _async;
        private Action _onComplete;

        [Inject]
        public SceneChangePresenter(SceneChangeView view, ManagerSceneController managerSceneController)
        {
            _view = view;
            _managerSceneController = managerSceneController;
        }

        public async UniTask LoadNextScene(string sceneName, float minimumLoadTime)
        {
            await _view.FadeOut();

            _view.SetLoadingUIActive(true);
            var startTime = Time.time;
            var targetProgress = 0f;
            var displayProgress = 0f;

            await _managerSceneController.ChangeScene(sceneName);
            _async = SceneManager.LoadSceneAsync(sceneName);

            while (Time.time - startTime < minimumLoadTime || _async is { isDone: false })
            {
                if (_async != null)
                {
                    targetProgress = Mathf.Clamp01(_async.progress);
                }

                displayProgress = Mathf.MoveTowards(displayProgress, targetProgress, Time.deltaTime / minimumLoadTime);
                _view.UpdateProgress(displayProgress);

                await UniTask.DelayFrame(1);
            }

            _view.SetLoadingUIActive(false);
            _onComplete?.Invoke();

            await _view.FadeIn();
        }

        public void SetOnComplete(Action onComplete)
        {
            _onComplete = onComplete;
        }

        public void Initialize()
        {
            // 必要な初期化処理をここに記述
        }
    }
}
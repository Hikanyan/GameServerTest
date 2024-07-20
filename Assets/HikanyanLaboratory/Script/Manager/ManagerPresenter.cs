using HikanyanLaboratory.System;
using Unity.VisualScripting;
using UnityEngine;

namespace HikanyanLaboratory.Manager
{
    public class ManagerPresenter : IInitializable
    {
        private readonly SceneChangePresenter _sceneChangePresenter;

        public ManagerPresenter(SceneChangePresenter sceneChangePresenter)
        {
            _sceneChangePresenter = sceneChangePresenter;
        }

        public void Initialize()
        {
            // 子コンテナでの初期化処理
            // 必要な処理をここに記述
        }

        public async void LoadNextScene(string sceneName, float minimumLoadTime)
        {
            await _sceneChangePresenter.LoadNextScene(sceneName, minimumLoadTime);
        }
    }
}
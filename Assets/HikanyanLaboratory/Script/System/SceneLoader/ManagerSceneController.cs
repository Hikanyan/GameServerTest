using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.System
{
    public class ManagerSceneController
    {
        private Scene _lastScene;
        private readonly SceneLoader _sceneLoader;
        private readonly Scene _neverUnloadScene;
        private readonly IFadeStrategy _fadeStrategy;
        private Stack<string> _sceneHistory = new Stack<string>();

        public ManagerSceneController(SceneLoader sceneLoader, Scene neverUnloadScene, IFadeStrategy fadeStrategy)
        {
            _sceneLoader = sceneLoader;
            _neverUnloadScene = neverUnloadScene;
            _fadeStrategy = fadeStrategy;
        }

        public async UniTask LoadSceneWithFade(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease, bool recordHistory = true)
        {
            await _fadeStrategy.FadeOut(fadeMaterial, fadeDuration, cutoutRange, ease);
            await _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (recordHistory && !_sceneLoader.IsSceneLoaded(sceneName))
            {
                _sceneHistory.Push(sceneName);
            }

            await _fadeStrategy.FadeIn(fadeMaterial, fadeDuration, cutoutRange, ease);
        }

        public async UniTask UnloadSceneWithFade(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease)
        {
            await _fadeStrategy.FadeOut(fadeMaterial, fadeDuration, cutoutRange, ease);
            await _sceneLoader.UnloadSceneAsync(sceneName);
            await _fadeStrategy.FadeIn(fadeMaterial, fadeDuration, cutoutRange, ease);
        }

        public async UniTask ReloadSceneWithFade(Material fadeMaterial, float fadeDuration, float cutoutRange,
            Ease ease)
        {
            if (_sceneHistory.Count > 0)
            {
                string currentScene = _sceneHistory.Pop();
                await UnloadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease);
                await LoadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease, false);
            }
        }

        public async UniTask LoadPreviousSceneWithFade(Material fadeMaterial, float fadeDuration, float cutoutRange,
            Ease ease)
        {
            if (_sceneHistory.Count > 1)
            {
                string currentScene = _sceneHistory.Pop();
                string previousScene = _sceneHistory.Peek();
                await UnloadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease);
                await LoadSceneWithFade(previousScene, fadeMaterial, fadeDuration, cutoutRange, ease, false);
            }
        }
    }
}
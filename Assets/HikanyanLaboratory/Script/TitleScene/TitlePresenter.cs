using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HikanyanLaboratory.System;
using UnityEngine;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitlePresenter
    {
        private readonly SceneManager _sceneManager;
        private readonly string _nextSceneName;
        private readonly TitleController _titleController;
        private readonly Material _fadeMaterial;
        private readonly float _fadeDuration;
        private readonly float _cutoutRange;
        private readonly Ease _fadeEase;

        public TitlePresenter(SceneManager sceneManager, TitleController titleController, Material fadeMaterial,
            float fadeDuration, float cutoutRange, Ease fadeEase)
        {
            _sceneManager = sceneManager;
            _titleController = titleController;
            _fadeMaterial = fadeMaterial;
            _fadeDuration = fadeDuration;
            _cutoutRange = cutoutRange;
            _fadeEase = fadeEase;
        }

        public async void OnStartButtonPressed()
        {
            await _sceneManager.LoadSceneWithFade(_nextSceneName, _fadeMaterial, _fadeDuration, _cutoutRange,
                _fadeEase);
        }
    }
}
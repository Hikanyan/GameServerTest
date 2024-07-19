using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.System
{
    public class LoadingSceneView
    {
        [SerializeField] private GameObject _loadingUI;
        [SerializeField] private Material _fadeMaterial;
        [SerializeField] private Texture _maskTexture;
        [SerializeField, Range(0, 1)] private float _cutoutRange;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fadeDuration = 1.0f;
        private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
        private static readonly int Range1 = Shader.PropertyToID("_Range");

        private void Start()
        {
            _fadeMaterial.SetTexture(MaskTex, _maskTexture);
        }

        public void SetLoadingUIActive(bool isActive)
        {
            _loadingUI.SetActive(isActive);
        }

        public async UniTask FadeOut()
        {
            DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 1, _fadeDuration)
                .OnUpdate(() => _fadeMaterial.SetFloat(Range1, 1 - _cutoutRange))
                .SetEase(Ease.Linear);
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));
        }

        public async UniTask FadeIn()
        {
            DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 0, _fadeDuration)
                .OnUpdate(() => _fadeMaterial.SetFloat(Range1, 1 - _cutoutRange))
                .SetEase(Ease.Linear);
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));
        }

        public void UpdateProgress(float progress)
        {
            _slider.value = progress;
        }
    }
}
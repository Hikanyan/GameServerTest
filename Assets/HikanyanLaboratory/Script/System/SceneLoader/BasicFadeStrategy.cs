using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace HikanyanLaboratory.System
{
    public class BasicFadeStrategy : IFadeStrategy
    {
        private readonly Material _fadeMaterial;
        private readonly float _fadeDuration;
        private float _cutoutRange;

        private static readonly int Range1 = Shader.PropertyToID("_Range");

        public BasicFadeStrategy(Material fadeMaterial, float fadeDuration, float cutoutRange)
        {
            _fadeMaterial = fadeMaterial;
            _fadeDuration = fadeDuration;
            _cutoutRange = cutoutRange;
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
    }
}
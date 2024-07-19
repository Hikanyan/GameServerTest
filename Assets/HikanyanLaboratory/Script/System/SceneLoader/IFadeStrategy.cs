using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace HikanyanLaboratory.System
{
    public interface IFadeStrategy
    {
        UniTask FadeOut(Material fadeMaterial, float fadeDuration, float cutoutRange, Ease ease);
        UniTask FadeIn(Material fadeMaterial, float fadeDuration, float cutoutRange, Ease ease);
    }
}
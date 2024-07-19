using Cysharp.Threading.Tasks;

namespace HikanyanLaboratory.System
{
    public interface IFadeStrategy
    {
        UniTask FadeOut();
        UniTask FadeIn();
    }
}
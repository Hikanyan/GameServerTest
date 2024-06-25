using HikanyanLaboratory.Script.TitleScene;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.LifeTimeScope
{
    public class TitleLifeTimeScope　: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TitleController>(Lifetime.Singleton);
            builder.Register<TitlePresenter>(Lifetime.Singleton);
            builder.Register<TitleView>(Lifetime.Singleton);
        }
    }
}
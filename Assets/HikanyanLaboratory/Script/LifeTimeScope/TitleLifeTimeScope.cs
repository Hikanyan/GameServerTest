using HikanyanLaboratory.Script.TitleScene;
using HikanyanLaboratory.System;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.LifeTimeScope
{
    public class TitleLifeTimeScope　: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // 子コンテナ
            base.Configure(builder);
            // 親コンテナからManagerSceneControllerを取得
            var parentLifetimeScope = Parent;
            if (parentLifetimeScope != null)
            {
                var parentContainer = parentLifetimeScope.Container;
                var sceneController = parentContainer.Resolve<ManagerSceneController>();
                builder.RegisterInstance(sceneController);
            }

            builder.Register<TitleController>(Lifetime.Singleton);
            builder.Register<TitleUIManager>(Lifetime.Singleton);
            builder.Register<TitlePresenter>(Lifetime.Singleton);
        }
    }
}
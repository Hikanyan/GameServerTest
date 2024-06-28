using HikanyanLaboratory.Network;
using HikanyanLaboratory.Script.TitleScene;
using HikanyanLaboratory.System;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    public class TitleLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // // 子コンテナ
            // base.Configure(builder);
            // // 親コンテナからManagerSceneControllerを取得
            // var parentLifetimeScope = Parent;
            // if (parentLifetimeScope != null)
            // {
            //     var parentContainer = parentLifetimeScope.Container;
            //     var sceneController = parentContainer.Resolve<ManagerSceneController>();
            //     builder.RegisterInstance(sceneController);
            // }


            // Sceneの依存関係を登録
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ManagerSceneController>(Lifetime.Singleton);

            // TitleSceneの依存関係を登録
            builder.Register<TitlePresenter>(Lifetime.Singleton);
            builder.Register<TitleController>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<TitleUIManager>();
            builder.RegisterComponentInHierarchy<ServiseLogin>();

            // 開始処理
            builder.RegisterEntryPoint<TitlePresenter>();
        }
    }
}
using HikanyanLaboratory.Manager;
using HikanyanLaboratory.SequenceSystem;
using HikanyanLaboratory.System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    /// <summary>
    /// Root LifetimeScope for Manager Scene.
    /// 全体のSceneに反映されるLifetimeScope
    /// 親コンテナ
    /// 子のコンテナは見ないようにしないと循環参照になるので注意
    /// </summary>
    public class ManagerLifetimeScope : LifetimeScope
    {
        [SerializeField] private bool _isDebugMode;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ManagerSceneController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<SceneChangeView>();

            builder.Register<SequenceManager>(Lifetime.Singleton);
            builder.Register<SceneChangePresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ManagerPresenter>().WithParameter("isDebugMode", _isDebugMode);
        }
    }
}
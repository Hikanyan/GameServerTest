using HikanyanLaboratory.Manager;
using HikanyanLaboratory.System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    public class ManagerLifetimeScope : LifetimeScope
    {
        [SerializeField] private bool _isDebugMode;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ManagerSceneController>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<ManagerPresenter>().WithParameter("isDebugMode", _isDebugMode);
        }
    }
}
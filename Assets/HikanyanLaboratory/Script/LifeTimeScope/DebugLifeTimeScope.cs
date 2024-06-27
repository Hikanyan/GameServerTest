using HikanyanLaboratory.Debug;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    public class DebugLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DebugManager>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<DebugClass>();
        }
    }
}
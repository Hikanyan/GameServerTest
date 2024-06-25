using HikanyanLaboratory.Network;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.LifeTimeScope
{
    public class LobbyScope : LootLifeTimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LobbyInitializer>();
            builder.Register<PlayFabAuthService>(Lifetime.Singleton);
            builder.Register<PlayFabController>(Lifetime.Singleton);
            builder.Register<ServiseLogin>(Lifetime.Singleton);
            builder.Register<LobbyPresenter>(Lifetime.Singleton);
        }
    }
}
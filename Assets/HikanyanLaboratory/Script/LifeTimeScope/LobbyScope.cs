using HikanyanLaboratory.Script.Network;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.LifeTimeScope
{
    public class LobbyScope : LootLifeTimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LobbyInitializer>();
        }
    }
}
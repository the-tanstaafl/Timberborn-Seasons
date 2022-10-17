using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace FloodSeason.NotificationSystem;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonNotificationSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonNotificationBus>().AsSingleton();
    }
}
using Bindito.Core;
using FloodSeason.Events;
using FloodSeason.Weather;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.NotificationSystem;
using Timberborn.TemplateSystem;

namespace FloodSeason.Seasons;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        //containerDefinition.Bind<SeasonNotificationBus>().AsSingleton();
        containerDefinition.MultiBind<TemplateModule>().ToProvider<SeasonTemplateModuleProvider>().AsSingleton();
    }
}
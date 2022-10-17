using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace FloodSeason.Terrain;

[Configurator(SceneEntrypoint.InGame)]
public class TerrainTextureServiceConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<TerrainTextureService>().AsSingleton();
    }
}
using Bindito.Core;
using Bindito.Unity;
using Timberborn.Debugging;
using Timberborn.SkySystem;

namespace FloodSeason.SkySystem;

public class SeasonSkySystemConfigurator : PrefabConfigurator
{
    public override void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonSun>().ToInstance(GetInstanceFromPrefab<SeasonSun>());
        containerDefinition.Bind<SeasonDayStageCycle>().ToInstance(GetInstanceFromPrefab<SeasonDayStageCycle>());
        containerDefinition.MultiBind<IConsoleModule>().To<SkySystemConsoleModule>().AsSingleton();
    }
}
using Bindito.Core;
using System;
using System.Collections.Generic;
using System.Text;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace FloodSeason.Growing
{
    [Configurator(SceneEntrypoint.InGame)]
    public class GrowingConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<SeasonChangeListener>().AsSingleton();
        }
    }
}

using Bindito.Core;
using FloodSeason.UI;
using HarmonyLib;
using Timberborn.Debugging;
using Timberborn.WeatherSystemUI;
using WeatherPanel = Timberborn.WeatherSystemUI.WeatherPanel;

namespace FloodSeason.Patches;

[HarmonyPatch(typeof(WeatherSystemUIConfigurator), nameof(WeatherSystemUIConfigurator.Configure))]
public class WeatherSystemUIConfiguratorPatch
{
    static bool Prefix(IContainerDefinition containerDefinition)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Altering WeatherSystemUIConfigurator...");
        containerDefinition.Bind<SeasonDatePanel>().AsSingleton();
        containerDefinition.Bind<DroughtNotifier>().AsSingleton();
        containerDefinition.Bind<SeasonFastForwarder>().AsSingleton();
        containerDefinition.Bind<SeasonWeatherPanel>().AsSingleton();
        containerDefinition.MultiBind<IConsoleModule>().To<SeasonFastForwarder>().AsSingleton();
        return false;
    }
}
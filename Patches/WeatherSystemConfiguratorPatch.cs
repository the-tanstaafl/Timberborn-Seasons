using Bindito.Core;
using FloodSeason.Seasons;
using FloodSeason.Weather;
using HarmonyLib;
using Timberborn.WeatherSystem;

namespace FloodSeason.Patches;

[HarmonyPatch(typeof(WeatherSystemConfigurator), nameof(WeatherSystemConfigurator.Configure))]

public class WeatherSystemConfiguratorPatch
{
    static void Prefix(IContainerDefinition containerDefinition)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Altering {nameof(WeatherSystemConfiguratorPatch)}...");
    }
}
using HarmonyLib;
using Timberborn.WeatherSystem;

namespace FloodSeason.Patches;

[HarmonyPatch]
public class WeatherServicePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WeatherService), nameof(WeatherService.OnDaytimeStart))]
    static bool OnDaytimeStart()
    {
        return false;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WeatherService), nameof(WeatherService.StartNextCycle))]
    static bool StartNextCycle()
    {
        return false;
    }
}
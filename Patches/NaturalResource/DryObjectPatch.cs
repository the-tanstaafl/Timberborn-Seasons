using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using HarmonyLib;
using Timberborn.SoilMoistureSystem;

namespace FloodSeason.Patches.NaturalResource;

[HarmonyPatch]
public class DryObjectPatch
{
    /*[HarmonyPatch(typeof(DryObject), nameof(DryObject.IsDry), MethodType.Getter)]
    [HarmonyPrefix]
    static bool IsDry(DryObject __instance, ref bool __result)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();

        if (seasonService.CurrentSeason is Winter)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Winter IsDry");
            __result = true;
            return false;
        }
        return true;
    }*/
    
    [HarmonyPatch(typeof(DryObject), nameof(DryObject.ExitDryState))]
    [HarmonyPrefix]
    static void ExitDryState(DryObject __instance)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        if (!__instance.IsDry || seasonService.CurrentSeason is Winter)
            return;
        __instance.InternalExitDryState();
    }
    
    /*[HarmonyPatch(typeof(DryObject), nameof(DryObject.InternalEnterDryState))]
    [HarmonyPostfix]
    static void InternalEnterDryState(DryObject __instance)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"InternalEnterDryState called{__instance.IsDry}");
    }*/
}
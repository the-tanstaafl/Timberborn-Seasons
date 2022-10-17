using FloodSeason.Growing;
using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using HarmonyLib;
using Timberborn.NaturalResourcesLifeCycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.TimeSystem;

namespace FloodSeason.Patches.NaturalResource;

/*[HarmonyPatch]
public class WateredNaturalResourcePatch
{
    [HarmonyPatch(typeof(WateredNaturalResource), nameof(WateredNaturalResource.StartDryingOut))]
    [HarmonyPostfix]
    static void StartDryingOut(ref LivingNaturalResource ____livingNaturalResource, ref ITimeTrigger ____timeTrigger)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo("StartDryingOut");
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        if (!____livingNaturalResource.IsDead && seasonService.CurrentSeason is Winter)
            ____timeTrigger.Resume();
    }
    
    [HarmonyPatch(typeof(WateredNaturalResource), nameof(WateredNaturalResource.StopDryingOut))]
    [HarmonyPostfix]
    static void StopDryingOut(ref LivingNaturalResource ____livingNaturalResource, ref ITimeTrigger ____timeTrigger)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo("StopDryingOut");
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        if (!____livingNaturalResource.IsDead && seasonService.CurrentSeason is Winter)
            ____timeTrigger.Resume();
    }
}*/
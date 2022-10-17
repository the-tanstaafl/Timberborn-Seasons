using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using HarmonyLib;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifeCycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.NaturalResourcesMoistureUI;
using Timberborn.SoilMoistureSystem;

namespace FloodSeason.Patches.NaturalResource;

[HarmonyPatch(typeof(NaturalResourceMoistureFragment), nameof(NaturalResourceMoistureFragment.BuildDescription))]
public class NaturalResourceMoistureFragmentPatch
{
    static void Postfix(ref string __result, NaturalResourceMoistureFragment __instance,
        ref WateredNaturalResource ____wateredNaturalResource, DryObject ____dryObject,
        LivingNaturalResource ____livingNaturalResource, LivingWaterNaturalResource ____livingWaterNaturalResource)
    {
        if (____wateredNaturalResource is null)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("WateredNaturalResource is null!");
            return;
        }

        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        var iLoc = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<ILoc>();

        if (____livingNaturalResource.IsDead)
        {
            __result = __instance.DescribeDeadResource();
            return;
        }
        if (seasonService.CurrentSeason is Winter)
        {
            __result = iLoc.T("seasons.growable.paused", $"{____wateredNaturalResource.DaysLeft:0.0}");
            return;
        }
        if (____dryObject && ____dryObject.IsDry)
        {
            __instance.DescribeDryResource();
            return;
        }

        __result = ____livingWaterNaturalResource && ____livingWaterNaturalResource.IsDying
            ? __instance.DescribeParchedOrFloodedResource()
            : iLoc.T(NaturalResourceMoistureFragment.WateredLocKey);
    }
}
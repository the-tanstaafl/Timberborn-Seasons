using FloodSeason.Seasons;
using HarmonyLib;
using Timberborn.SkySystem;
using UnityEngine;

namespace FloodSeason.Patches;

[HarmonyPatch(typeof(DayStageCycle), nameof(DayStageCycle.Transition), typeof(DayStage), typeof(DayStage), typeof(float))]
public class DayStageCyclePatch
{
    static void Postfix(DayStage currentDayStage,
        DayStage nextDayStage,
        float hoursToNextDayStage, ref DayStageTransition __result, DayStageCycle __instance)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        float transitionProgress = Mathf.SmoothStep(0.0f, 1f, 1f - Mathf.Clamp01(hoursToNextDayStage / __instance._transitionLengthInHours));
        bool isDrought = seasonService.CurrentSeason.Name.Equals("Summer");
        __result = new DayStageTransition(currentDayStage, isDrought, nextDayStage, false, transitionProgress);
    }
}
using Timberborn.Buildings;
using Timberborn.Fields;
using Timberborn.Forestry;
using Timberborn.Growing;
using Timberborn.NaturalResourcesLifeCycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.Planting;
using Timberborn.SoilMoistureSystem;
using UnityEngine;

namespace FloodSeason.Growing;

public class PlantableService
{
    //TODO deal with trees and berries
    public void PauseGrowth()
    {
        var crops = Object.FindObjectsOfType<Crop>();
        //var farmhouses = Object.FindObjectsOfType<FarmHouse>();
        //foreach (var farmHouse in farmhouses)
        //{
        //    var pausableBuilding = farmHouse.GetComponentInParent<PausableBuilding>();
        //    pausableBuilding.Pause();
        //}
        SeasonsPlugin.ConsoleWriter.LogInfo($"Amount: {crops.Length}");
        foreach (var crop in crops)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name}");
            var dryObject = crop.GetComponentInParent<DryObject>();
            //var living = crop.GetComponentInParent<LivingNaturalResource>();
            if (dryObject is not null)
            {
                dryObject.EnterDryState();
            }
            else
            {
                SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name} does not have {nameof(Crop)}");
            }
        }
    }

    public void ResumeGrowth()
    {
        var crops = Object.FindObjectsOfType<Crop>();
        //var farmhouses = Object.FindObjectsOfType<FarmHouse>();
        //foreach (var farmHouse in farmhouses)
        //{
        //    var pausableBuilding = farmHouse.GetComponentInParent<PausableBuilding>();
        //    pausableBuilding.Resume();
        //}
        foreach (var crop in crops)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name}");
            var dryObject = crop.GetComponentInParent<DryObject>();
            //var living = crop.GetComponentInParent<LivingNaturalResource>();
            if (dryObject is not null)
            {
                dryObject.ExitDryState();
            }
            else
            {
                SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name} does not have {nameof(Crop)}");
            }
        }
    }
}
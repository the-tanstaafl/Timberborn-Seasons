using System;
using FloodSeason.Events;
using FloodSeason.Growing;
using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.Debugging;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace FloodSeason.Weather;

public class SeasonWeatherService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey WeatherServiceKey = new(nameof(SeasonWeatherService));

    private readonly EventBus _eventBus;
    private readonly SeasonService _seasonService;
    private readonly MapEditorMode _mapEditorMode;
    private readonly ForecastService _forecastService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;
    private readonly PlantableService _plantableService;
    private readonly TerrainMeshManager _meshManager;

    public SeasonWeatherService(
        EventBus eventBus,
        SeasonService seasonService,
        MapEditorMode mapEditorMode,
        ForecastService forecastService,
        SeasonCycleTrackerService seasonCycleTrackerService, PlantableService plantableService)
    {
        _eventBus = eventBus;
        _seasonService = seasonService;
        _mapEditorMode = mapEditorMode;
        _forecastService = forecastService;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _plantableService = plantableService;
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        IObjectSaver singleton = singletonSaver.GetSingleton(WeatherServiceKey);
        /*singleton.Set(CurrentWeatherDurationKey, CurrentWeather.Duration);
        singleton.Set(CurrentWeatherProgressKey, CurrentWeather.Progress);
        singleton.Set(CurrentWeatherTemperatureKey, CurrentWeather.Temperature);
        singleton.Set(CurrentWeatherMultiplierKey, CurrentWeather.StrengthMultiplier);
        singleton.Set(CurrentWeatherTypeKey, (int)CurrentWeather.WeatherType);*/
    }

    public void Load()
    {
        _eventBus.Register(this);
    }

    [OnEvent]
    public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent) => StartNextDay();

    private void StartNextYear()
    {
        _seasonCycleTrackerService.Year++;
        _seasonCycleTrackerService.Month = 1;
        _seasonCycleTrackerService.Day = 1;
        //Generate and set the first season
        _forecastService.Initialize(_seasonService.CurrentSeason);
        Debug.Log($"Year: {_seasonCycleTrackerService.Year}, Duration: {Season.TotalDuration}");
    }

    private void StartNextDay()
    {
        _seasonCycleTrackerService.Day++;
        _forecastService.NextDay();

        if (_seasonCycleTrackerService.Day == Season.MonthDuration)
        {
            if (_seasonCycleTrackerService.Month % Season.SeasonDuration == 0)
            {
                _seasonService.NextSeason();
                if (_seasonService.CurrentSeason.Growing)
                {
                    SeasonsPlugin.ConsoleWriter.LogInfo("Resume Growth");
                    _plantableService.ResumeGrowth();
                }
                else
                {
                    SeasonsPlugin.ConsoleWriter.LogInfo("Pause Growth");
                    _plantableService.PauseGrowth();
                }
            }
            if (_seasonCycleTrackerService.Month == _seasonService.Seasons.Count * Season.SeasonDuration)
            {
                StartNextYear();
            }

            _seasonCycleTrackerService.Month++;
            _seasonCycleTrackerService.Day = 1;
        }
    }

    public ConsoleModuleDefinition GetDefinition() => new ConsoleModuleDefinition.Builder().AddMethod(
        new ConsoleMethod(
            "Cycle Season",
            () => { _seasonService.NextSeason(); })
    ).Build();
}
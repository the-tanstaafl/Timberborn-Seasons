using System;
using System.Collections;
using System.Collections.Generic;
using FloodSeason.Events;
using FloodSeason.Exceptions;
using FloodSeason.Seasons.Types;
using FloodSeason.Terrain;
using FloodSeason.Weather;
using Timberborn.AssetSystem;
using Timberborn.Core;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

namespace FloodSeason.Seasons;

public class SeasonService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey SeasonServiceKey = new SingletonKey(nameof(SeasonService));
    private static readonly PropertyKey<string> CurrentSeasonKey = new PropertyKey<string>(nameof(CurrentSeason));

    private static readonly int DesertTextureProperty = Shader.PropertyToID("_DesertTex");

    private readonly List<Season> _seasons;
    public Season CurrentSeason { get; set; }
    private IEnumerator<Season> _enumerator;
    private readonly ISingletonLoader _singletonLoader;
    private readonly EventBus _eventBus;
    private readonly MapEditorMode _mapEditorMode;
    private readonly ForecastService _forecastService;

    public bool IsActive { get; set; } = false;

    //TODO return immutable collection
    public List<Season> Seasons => _seasons;
    private Texture _defaultTexture;

    public SeasonService(ISingletonLoader singletonLoader, EventBus eventBus, MapEditorMode mapEditorMode,
        ForecastService forecastService)
    {
        _seasons = new List<Season>();
        //TODO properly register seasons
        Register(new Spring());
        Register(new Summer());
        Register(new Autumn());
        Register(new Winter());
        _enumerator = _seasons.GetEnumerator();
        _singletonLoader = singletonLoader;
        _eventBus = eventBus;
        _mapEditorMode = mapEditorMode;
        _forecastService = forecastService;
    }

    public void Register(Season season)
    {
        if (IsActive)
        {
            throw new InvalidStateException("Cannot register new seasons while the season manager is running");
        }

        Seasons.Add(season);
    }

    public void Unregister(Season season)
    {
        if (IsActive)
        {
            throw new InvalidStateException("Cannot unregister seasons while the season manager is running");
        }

        Seasons.Remove(season);
    }

    public void NextSeason()
    {
        if (!_enumerator.MoveNext())
        {
            _enumerator.Reset();
            _enumerator.MoveNext();
        }

        Update(_enumerator.Current);
    }

    private void Update(Season current)
    {
        CurrentSeason = current;
        _eventBus.Post(new SeasonChangedEvent(CurrentSeason));
        _forecastService.Initialize(current);
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        singletonSaver.GetSingleton(SeasonServiceKey).Set(CurrentSeasonKey, CurrentSeason.Name);
    }

    public void Load()
    {
        if (!_singletonLoader.HasSingleton(SeasonServiceKey))
        {
            NextSeason();
            return;
        }
        CurrentSeason = Seasons.Find(season =>
            season.Name.Equals(_singletonLoader.GetSingleton(SeasonServiceKey).Get(CurrentSeasonKey)));
        while (CurrentSeason != _enumerator.Current)
        {
            _enumerator.MoveNext();
        }
        Update(CurrentSeason);
    }
}
using System;
using FloodSeason.Seasons;
using FloodSeason.Weather;
using Timberborn.CoreUI;
using Timberborn.GameSound;
using Timberborn.GameUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WellbeingUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace FloodSeason.UI;

public class SeasonWeatherPanel : ILoadableSingleton, IUpdatableSingleton
{
    private static readonly string UnknownLocKey = "Weather.Forecast.Unknown";
    private static readonly string DroughtApproachingLocKey = "Weather.Forecast.DroughtApproaching";
    private static readonly string DroughtInProgressLocKey = "Weather.Forecast.DroughtInProgress";
    private static readonly string CounterLocKey = "Weather.Counter";
    private static readonly float DaysOfDroughtApproaching = 3f;
    private static readonly int NumberOfBlinks = 2;
    private static readonly float SecondsBetweenBlinks = 0.5f;
    private static readonly string BlinkingClass = "weather-panel--blink";
    private static readonly string DryClass = "weather-panel--dry";
    private static readonly string SummerClass = "weather-panel--summer";
    private static readonly string SpringClass = "weather-panel--spring";
    private static readonly string AutumnClass = "weather-panel--autumn";
    private static readonly string WinterClass = "weather-panel--winter";
    private static readonly string DryIncomingClass = "weather-panel--dry-incoming";
    private readonly GameLayout _gameLayout;
    private readonly VisualElementLoader _visualElementLoader;
    private readonly SeasonService _seasonService;
    private readonly SeasonWeatherService _weatherService;
    private readonly ILoc _loc;
    private readonly GameUISoundController _gameUISoundController;
    private readonly ITooltipRegistrar _tooltipRegistrar;
    private readonly PanelStack _panelStack;
    private readonly IDayNightCycle _dayNightCycle;
    private VisualElement _root;
    private Label _forecastCounter;
    private VisualElement _forecastIcon;
    private Timberborn.CoreUI.ProgressBar _progress;
    private float _secondsToNextBlink;
    private int _remainingBlinks;
    private bool _midBlink;
    private bool _pausedUntilTimeUnpaused;
    private string _tooltipText;
    private readonly ForecastService _forecastService;
    private Button _forecast;

    public SeasonWeatherPanel(
        GameLayout gameLayout,
        VisualElementLoader visualElementLoader,
        SeasonService seasonService,
        SeasonWeatherService weatherService,
        ForecastService forecastService,
        ILoc loc,
        GameUISoundController gameUISoundController,
        ITooltipRegistrar tooltipRegistrar,
        PanelStack panelStack,
        IDayNightCycle dayNightCycle)
    {
        _gameLayout = gameLayout;
        _visualElementLoader = visualElementLoader;
        _seasonService = seasonService;
        _weatherService = weatherService;
        _forecastService = forecastService;
        _loc = loc;
        _gameUISoundController = gameUISoundController;
        _tooltipRegistrar = tooltipRegistrar;
        _panelStack = panelStack;
        _dayNightCycle = dayNightCycle;
    }

    public void Load()
    {
        _root = _visualElementLoader.LoadVisualElement("Master/WeatherPanel");
        _tooltipRegistrar.Register(_root, () => _tooltipText);
        _progress = _root.Q<Timberborn.CoreUI.ProgressBar>("Progress");
        _forecastCounter = _root.Q<Label>("ForecastCounter");
        _forecastIcon = _root.Q<VisualElement>("ForecastIcon");
        _gameLayout.AddTopRight(_root, 6);
        UpdatePanel();
        _pausedUntilTimeUnpaused = true;
    }

    public void UpdateSingleton()
    {
        if (_pausedUntilTimeUnpaused && Time.deltaTime > 0.0)
            _pausedUntilTimeUnpaused = false;
        if (_pausedUntilTimeUnpaused)
            return;
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        /*var currentWeather = _forecastService.CurrentWeather;
        var duration = currentWeather.Duration;
        var partial = duration - _weatherService.PartialCycleDay;
        //var remaining = duration - partial;
        float progressBarValue = duration / partial;
        SetPanelContent($"{_forecastService.CurrentWeather}", progressBarValue, _weatherService);
        _root.AddToClassList(DryClass);

        switch (_forecastService.CurrentWeather.WeatherType)
        {
            case WeatherType.Drought:
            {
                _root.AddToClassList(DryClass);
                break;
            }
            case WeatherType.Sun:
                _root.AddToClassList(DryClass);
                break;
            case WeatherType.Rain:
                _root.RemoveFromClassList(DryClass);
                break;
            case WeatherType.Wind:
                _root.RemoveFromClassList(DryClass);
                break;
            case WeatherType.Flood:
                _root.RemoveFromClassList(DryClass);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }*/
        var currentWeather = _forecastService.CurrentWeather;
        _tooltipText =
            $"{currentWeather.WeatherType} {currentWeather.Progress + _dayNightCycle.DayProgress:F1}/{currentWeather.Duration}";
        var remaining = currentWeather.Duration - (currentWeather.Progress + _dayNightCycle.DayProgress);
        _forecastCounter.text =
            _loc.T(CounterLocKey, remaining.ToString("F1"));
    }

    private void SetPanelContent(
        string forecast,
        float progressBarValue,
        float duration)
    {
        _progress.SetProgress(Math.Max(progressBarValue, 0.0f));
        //_root.EnableInClassList(BlinkingClass, blink);
        //_forecastCounter.parent.ToggleDisplayStyle(forecastCount > 0.0);
        _forecastCounter.text = _loc.T(CounterLocKey, duration.ToString("F1"));
        //_forecastIcon.EnableInClassList(DryIncomingClass, droughtIncoming);
        _tooltipText = forecast;
    }

    private void StartBlinking()
    {
        _remainingBlinks = NumberOfBlinks * 2 - 1;
        _midBlink = true;
        _secondsToNextBlink = SecondsBetweenBlinks + Time.unscaledDeltaTime;
        _gameUISoundController.PlayBlinkingSound();
    }

    private bool NextBlinkingBarState()
    {
        _secondsToNextBlink -= Time.unscaledDeltaTime;
        if (_secondsToNextBlink <= 0.0)
        {
            _secondsToNextBlink = SecondsBetweenBlinks;
            --_remainingBlinks;
            _midBlink = !_midBlink;
        }

        return _midBlink;
    }
}
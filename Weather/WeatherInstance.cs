using System;
using FloodSeason.Exceptions;

namespace FloodSeason.Weather;

public class WeatherInstance
{
    public WeatherType WeatherType { get; }
    public int Duration { get; }
    public int Progress { get; private set; }
    public int Temperature { get; }
    public int StartDay { get; }
    public int EndDay => StartDay + Duration;
    public float StrengthMultiplier { get; }

    public WeatherInstance(WeatherType weatherType, int startDay, int duration, int temperature,
        float strengthMultiplier)
    {
        WeatherType = weatherType;
        StartDay = startDay;
        Duration = duration;
        Temperature = temperature;
        StrengthMultiplier = strengthMultiplier;
    }

    public override string ToString()
    {
        return $"{WeatherType} ({Temperature}°C)";
    }

    public void NextDay()
    {
        if (Progress >= Duration)
        {
            throw new InvalidStateException("Duration of WeatherInstance Exceeded");
        }

        Progress++;
    }
}
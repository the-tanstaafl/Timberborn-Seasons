using FloodSeason.Seasons;
using FloodSeason.Weather;

namespace FloodSeason.Events;

public class WeatherChangedEvent
{
    public WeatherInstance WeatherInstance { get; }

    public WeatherChangedEvent(WeatherInstance weatherInstance)
    {
        WeatherInstance = weatherInstance;
    }
}
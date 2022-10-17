using FloodSeason.Seasons;

namespace FloodSeason.Weather.Modifiers;

public class WaterSourceModifier : IModifier
{
    public float Weight { get; }
    public float Multiplier { get; }
    public WeatherType WeatherType { get; }

    public WaterSourceModifier(float weight, float multiplier, WeatherType weatherType)
    {
        Weight = weight;
        Multiplier = multiplier;
        WeatherType = weatherType;
    }
}
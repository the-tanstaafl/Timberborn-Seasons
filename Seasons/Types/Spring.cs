using FloodSeason.Weather;
using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public class Spring : Season
{
    private static float _baseMultiplier = 1.5f;
    public override string Name => "Spring";
    public override int Temperature => 15;

    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.3f, _baseMultiplier * 0.8f, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier * 1.3f, WeatherType.Rain),
        new WaterSourceModifier(0.2f, _baseMultiplier * 4f, WeatherType.Flood),
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };
}
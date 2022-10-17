using FloodSeason.Weather;
using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public class Autumn : Season
{
    private static float _baseMultiplier = 1.2f;
    public override string Name => "Autumn";
    public override int Temperature => 15;
    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.2f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier * 1.3f, WeatherType.Rain),
        new WaterSourceModifier(0.2f, _baseMultiplier * 3f, WeatherType.Flood),
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };
}
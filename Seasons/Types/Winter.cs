using FloodSeason.Weather;
using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public class Winter : Season
{
    private static float _baseMultiplier = 0f;

    public override string Name => "Winter";
    public override int Temperature => -10;

    public override bool Growing => false;

    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier, WeatherType.Rain),
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };
}
using FloodSeason.Weather;
using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public class Summer : Season
{
    public override string Name => "Summer";
    public override int Temperature => 25;
    private static float _baseMultiplier = 0.2f;

    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.2f, _baseMultiplier * 0.5f, WeatherType.Sun),
        new WaterSourceModifier(0.8f, 0, WeatherType.Drought)
    };
}
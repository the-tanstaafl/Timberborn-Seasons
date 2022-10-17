namespace FloodSeason.Weather.Modifiers;

public interface IModifier
{
    public float Weight { get; }
    public float Multiplier { get; }

}
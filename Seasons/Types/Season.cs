using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public abstract class Season
{
    public static readonly int MonthDuration = 2; //Days in month
    public static readonly int SeasonDuration = 1; //Months in season
    public static readonly int TotalDuration = MonthDuration * SeasonDuration; //Months in season

    public abstract string Name { get; }
    public abstract int Temperature { get; }
    
    public int MaxDuration = TotalDuration / 2;
    public int MinDuration = 1;

    public abstract IModifier[] Modifiers { get; }
    public virtual bool Growing { get; } = true;

    public override string ToString()
    {
        return $"Season: [{Name} {Temperature}°C]";
    }
}
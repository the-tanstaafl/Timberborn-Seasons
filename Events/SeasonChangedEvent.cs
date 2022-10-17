using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;

namespace FloodSeason.Events;

public class SeasonChangedEvent
{
    public Season Season { get; }

    public SeasonChangedEvent(Season season)
    {
        Season = season;
    }
}
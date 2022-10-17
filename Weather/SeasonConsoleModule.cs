using FloodSeason.Seasons;
using Timberborn.Debugging;

namespace FloodSeason.Weather;

public class SeasonConsoleModule : IConsoleModule
{
    private readonly SeasonService _seasonService;

    public SeasonConsoleModule(SeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public ConsoleModuleDefinition GetDefinition() => new ConsoleModuleDefinition.Builder().AddMethod(
        new ConsoleMethod(
            "Cycle Season",
            () => { _seasonService.NextSeason(); })
    ).Build();
}
using Bindito.Core;
using FloodSeason.WaterLogic;
using Timberborn.TemplateSystem;
using Timberborn.WaterSourceSystem;
namespace FloodSeason.Seasons;

public class SeasonTemplateModuleProvider : IProvider<TemplateModule>
{
    public TemplateModule Get()
    {
        TemplateModule.Builder builder = new TemplateModule.Builder();
        builder.AddDecorator<WaterSource, SeasonWaterSourceController>();
        return builder.Build();
    }
}
using HarmonyLib;
using Timberborn.TemplateSystem;
using Timberborn.WaterSourceSystem;

namespace FloodSeason.Patches;

[HarmonyPatch(typeof(WaterSourceSystemConfigurator), nameof(WaterSourceSystemConfigurator.ProvideTemplateModule))]
public class WaterSourceSystemConfiguratorPatch
{
    /// <summary>
    /// Remove the <see cref="WaterSourceSystemConfigurator"/> from the <see cref="TemplateModule"/>
    /// </summary>
    static void Postfix(ref TemplateModule __result)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Skipping registering WaterSourceSystemConfigurator...");
        TemplateModule.Builder builder = new TemplateModule.Builder();
        __result = builder.Build();
    }
}
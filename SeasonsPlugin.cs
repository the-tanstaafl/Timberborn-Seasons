using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;

namespace FloodSeason
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class SeasonsPlugin : IModEntrypoint
    {
        public static IConsoleWriter ConsoleWriter;
        public void Entry(IMod mod, IConsoleWriter consoleWriter)
        {
            // Plugin startup logic
            ConsoleWriter = consoleWriter;
            var harmony = new Harmony("me.darkeyedragon.seasons");
            var assembly = Assembly.GetExecutingAssembly();
            //TimberAPI.AssetRegistry.AddSceneAssets(PluginInfo.PLUGIN_GUID, SceneEntryPoint.InGame);
            harmony.PatchAll(assembly);
            consoleWriter.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

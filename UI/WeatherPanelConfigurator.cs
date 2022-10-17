using System;
using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace FloodSeason.UI;
[Configurator(SceneEntrypoint.InGame)]
public class WeatherPanelConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<WeatherForecastPanel>().AsSingleton();
    }

    /*[HarmonyPatch(typeof(MainMenuPanel), "GetPanel")]
    public static class MainMenuPanelPatch
    {
        private static void Postfix(ref VisualElement __result)
        {
            VisualElement root = __result.Query("MainMenuPanel");
            UIBuilder uiBuilder = TimberAPI.DependencyContainer.GetInstance<UIBuilder>();
            LocalizableButton button = uiBuilder.Presets().Buttons()
                .Button("menu.uipreview", new Length(244, Pixel));
            button.clicked +=
                WeatherForecastPanel.OpenWeatherForecastPanel;
            uiBuilder.InitializeVisualElement(button);
            root.Insert(6, button);
        }
    }*/
}
using System;
using Timberborn.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.GameUI;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace FloodSeason.UI;

public class WeatherForecastPanel: IPanelController, ILoadableSingleton
{
    private readonly VisualElementInitializer _visualElementInitializer;
    private readonly IResourceAssetLoader _resourceAssetLoader;
    private readonly PanelStack _panelStack;
    private readonly GameLayout _gameLayout;
    private VisualElement _root;

    public WeatherForecastPanel(VisualElementInitializer visualElementInitializer, IResourceAssetLoader resourceAssetLoader, PanelStack panelStack, GameLayout gameLayout)
    {
        _visualElementInitializer = visualElementInitializer;
        _resourceAssetLoader = resourceAssetLoader;
        _panelStack = panelStack;
        _gameLayout = gameLayout;
    }

    public VisualElement GetPanel()
    {
        return _root;
    }

    public bool OnUIConfirmed() => false;

    public void OnUICancelled()
    {
        _panelStack.Pop(this);
    }

    public void Load()
    {
        /*_root = LoadVisualElement("UIForecastTemplate");
        var style = _resourceAssetLoader.Load<StyleSheet>($"{PluginInfo.PLUGIN_GUID}/seasons/UIForecastStyle");
        _root.styleSheets.Add(style);
        _gameLayout.AddAbsoluteItem(_root);*/
        //_root.Q<Button>("CancelButton").clicked += OnUICancelled;
    }
    
    private VisualElement LoadVisualElement(string elementName) => LoadVisualElement(LoadVisualTreeAsset(elementName));

    private VisualTreeAsset LoadVisualTreeAsset(string elementName) => _resourceAssetLoader.Load<VisualTreeAsset>( $"{PluginInfo.PLUGIN_GUID}/seasons/{elementName}");

    private VisualElement LoadVisualElement(VisualTreeAsset visualTreeAsset)
    {
        VisualElement visualElement = visualTreeAsset.CloneTree().ElementAt(0);
        _visualElementInitializer.InitializeVisualElement(visualElement);
        return visualElement;
    }
}
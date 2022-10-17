using FloodSeason.Events;
using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using Timberborn.AssetSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace FloodSeason.Terrain;

public class TerrainTextureService : IPostLoadableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey TerrainTextureServiceKey = new SingletonKey(nameof(TerrainTextureService));
    private static readonly PropertyKey<string> DryTextureKey = new PropertyKey<string>(nameof(DryTexture));
    private static readonly PropertyKey<string> SeasonTextureKey = new PropertyKey<string>(nameof(SeasonTexture));

    private readonly IResourceAssetLoader _resourceAssetLoader;
    private readonly TerrainMeshManager _terrainMeshManager;
    private readonly ISingletonLoader _singletonLoader;
    private readonly SeasonService _seasonService;
    private readonly EventBus _eventBus;

    private string _dryTexturePath;
    private string _seasonTexturePath;

    public Texture DryTexture { get; set; }
    public Texture SeasonTexture { get; set; }
    private static readonly int BaseAlbedoTex = Shader.PropertyToID("_BaseAlbedoTex");

    public TerrainTextureService(IResourceAssetLoader resourceAssetLoader, TerrainMeshManager terrainMeshManager,
        ISingletonLoader singletonLoader, SeasonService seasonService, EventBus eventBus)
    {
        _resourceAssetLoader = resourceAssetLoader;
        _terrainMeshManager = terrainMeshManager;
        _singletonLoader = singletonLoader;
        _seasonService = seasonService;
        _eventBus = eventBus;
    }

    [OnEvent]
    public void OnSeasonChangeEvent(SeasonChangedEvent changedEvent) => UpdateState(changedEvent.Season);

    public void UpdateState(Season season)
    {
        if (season is Winter)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Switch to Winter Terrain");
            Shader.SetGlobalTexture(TerrainMaterialMap.DesertTextureProperty, _resourceAssetLoader
                .Load<Material>($"{PluginInfo.PLUGIN_GUID}/seasons/Desert-Winter")
                .mainTexture);
        }
        else
        {
            if (Shader.GetGlobalTexture(TerrainMaterialMap.DesertTextureProperty) != DryTexture)
            {
                SeasonsPlugin.ConsoleWriter.LogInfo("Switch to Default Terrain");
                Shader.SetGlobalTexture(TerrainMaterialMap.DesertTextureProperty, DryTexture);
            }
        }

        UpdateTerrainMesh(season);
    }

    private void UpdateTerrainMesh(Season season)
    {
        foreach (var (key, value) in _terrainMeshManager._tiles)
        {
            var renderer = value.GetComponent<MeshRenderer>();
            var materials = renderer.materials;
            foreach (var material in materials)
            {
                if (material.name.StartsWith("Grass") || material.name.StartsWith("CliffEdge"))
                {
                    if (SeasonTexture == null)
                    {
                        SeasonTexture = material.GetTexture(BaseAlbedoTex);
                    }

                    if (season is not Spring && season is not null)
                    {
                        _seasonTexturePath = $"{PluginInfo.PLUGIN_GUID}/seasons/Grass-{season.Name}";
                        material.SetTexture(BaseAlbedoTex,
                            _resourceAssetLoader
                                .Load<Material>(_seasonTexturePath)
                                .mainTexture);
                    }
                    else
                    {
                        material.SetTexture(BaseAlbedoTex, SeasonTexture);
                        _seasonTexturePath = "";
                    }
                }
            }
        }
    }

    public void PostLoad()
    {
        DryTexture = Shader.GetGlobalTexture(TerrainMaterialMap.DesertTextureProperty);
        SeasonsPlugin.ConsoleWriter.LogInfo($"DryTexture: {DryTexture.name}");
        UpdateState(_seasonService.CurrentSeason);
    }

    public void Load()
    {
        _eventBus.Register(this);
    }
}
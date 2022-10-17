using Bindito.Core;
using Timberborn.CameraSystem;
using Timberborn.SingletonSystem;
using Timberborn.SkySystem;
using UnityEngine;

namespace FloodSeason.SkySystem;

public class SeasonSun : Sun
{
    [Inject]
    public void InjectDependencies(CameraComponent cameraComponent, SeasonDayStageCycle dayStageCycle)
    {
        _cameraComponent = cameraComponent;
        _dayStageCycle = dayStageCycle;
    }
}
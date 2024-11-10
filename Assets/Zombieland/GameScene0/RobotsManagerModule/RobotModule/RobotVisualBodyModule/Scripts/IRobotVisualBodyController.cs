using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule
{
    public interface IRobotVisualBodyController
    {
        event Action OnWeaponInSceneReady;

        GameObject RobotInScene { get; }
        GameObject WeaponInScene { get; }
        List<GameObject> SensorTriggersGameobject { get; }

        IRobotController RobotController { get; }
    }
}
using System;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotSpawnModule
{
    public interface IRobotSpawnController
    {
        event Action<Vector3, Quaternion> OnSpawn;

        IRobotController RobotController { get; }
    }
}
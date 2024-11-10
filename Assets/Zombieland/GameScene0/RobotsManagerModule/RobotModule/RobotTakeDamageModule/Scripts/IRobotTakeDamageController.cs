using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotTakeDamageModule
{
    public interface IRobotTakeDamageController
    {
        event Action<Vector3, Vector3> OnApplyImpact;

        IRobotController RobotController { get; }

        void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection);
    }
}
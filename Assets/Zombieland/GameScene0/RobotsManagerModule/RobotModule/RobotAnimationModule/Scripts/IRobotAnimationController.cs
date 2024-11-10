using System;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAnimationModule
{
    public interface IRobotAnimationController
    {
        event Action<Vector3> OnAnimatorMoveEvent;
        event Action<bool> OnAnimationAttack;
        event Action<string> OnAnimationCreateWeapon;
        event Action OnAnimationDestroyWeapon;
        event Action OnStep;

        IRobotController RobotController { get; }
    }
}
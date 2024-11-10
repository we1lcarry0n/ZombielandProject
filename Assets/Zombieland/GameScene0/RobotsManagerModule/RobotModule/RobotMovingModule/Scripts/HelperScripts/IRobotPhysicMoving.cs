using System;
using UnityEngine;



namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotMovingModule
{
    public interface IRobotPhysicMoving
    {
        event Action<float, bool> OnMoving;

        void Disable();
        void Init(IRobotMovingController robotMovingController);
        void ActivateMoving(bool isActive);
        void Move(Vector3 animatorRootPosition);
    }
}
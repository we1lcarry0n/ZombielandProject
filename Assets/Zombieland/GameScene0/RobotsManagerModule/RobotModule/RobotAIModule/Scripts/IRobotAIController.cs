using System;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAIModule
{
    public interface IRobotAIController
    {
        event Action<bool> OnFire;

        IRobotController RobotController { get; }
        bool IsPatrolling { get; }
    }
}
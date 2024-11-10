using System.Collections.Generic;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotImpactableSensorModule
{
    public interface IRobotImpactableSensorController
    {
        IRobotController RobotController { get; }
        List<Impactable> Impactables { get; }
    }
}
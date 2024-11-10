using System;

namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public interface IRobotDeadBodySensorController
    {
        event Action<IController> OnDeadBodyDetected;

        IRobotAwarenesController RobotAwarenesController { get; }

        void StartRobotDeadBodySensor();
        void StopRobotDeadBodySensor();
    }
}
using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.RobotsManagerModule
{
    public interface IRobotsManagerController
    {
        IRootController RootController { get; }
        List<IRobotController> ActiveRobotControllers { get; }

        void AddRobotToActive(IRobotController robotController);
        void RemoveRobotFromActive(IRobotController robotController);
    }
}
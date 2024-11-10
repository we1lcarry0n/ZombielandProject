using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAIModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAnimationModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotBuffDebuffModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotImpactableSensorModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotMovingModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotSpawnModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotTakeDamageModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule;

namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public interface IRobotController
    {
        IRobotsManagerController RobotsManagerController { get; }
        RobotSpawnData RobotSpawnData { get; }
        IRobotDataController RobotDataController { get; }
        IRobotVisualBodyController RobotVisualBodyController { get; }
        IRobotSpawnController RobotSpawnController { get; }
        IRobotMovingController RobotMovingController { get; }
        IRobotAnimationController RobotAnimationController { get; }
        IRobotTakeDamageController RobotTakeDamageController { get; }
        IRobotBuffDebuffController RobotBuffDebuffController { get; }
        IRobotImpactableSensorController RobotImpactableSensorController { get; }
        IRobotAIController RobotAIController { get; }
        IRobotAwarenesController RobotAwarenesController { get; }
    }
}
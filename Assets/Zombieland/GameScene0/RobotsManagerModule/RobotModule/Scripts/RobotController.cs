using System.Collections.Generic;
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
    public class RobotController : Controller, IRobotController
    {
        public IRobotsManagerController RobotsManagerController { get; private set; }
        public RobotSpawnData RobotSpawnData { get; private set; }
        public IRobotDataController RobotDataController { get; private set; }
        public IRobotVisualBodyController RobotVisualBodyController { get; private set; }
        public IRobotSpawnController RobotSpawnController { get; private set; }
        public IRobotMovingController RobotMovingController { get; private set; }
        public IRobotAnimationController RobotAnimationController { get; private set; }
        public IRobotTakeDamageController RobotTakeDamageController { get; private set; }
        public IRobotBuffDebuffController RobotBuffDebuffController { get; private set; }
        public IRobotImpactableSensorController RobotImpactableSensorController { get; private set; }
        public IRobotAIController RobotAIController { get; private set; }
        public IRobotAwarenesController RobotAwarenesController { get; private set; }

        public RobotController(IController parentController, List<IController> requiredControllers, RobotSpawnData robotSpawnData) : base(parentController, requiredControllers)
        {
            RobotsManagerController = parentController as IRobotsManagerController;
            RobotSpawnData = robotSpawnData;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            RobotDataController = new RobotDataController(this, null);
            subsystemsControllers.Add((IController)RobotDataController);

            RobotVisualBodyController = new RobotVisualBodyController(this, new List<IController> { (IController)RobotsManagerController.RootController.EnvironmentController });
            subsystemsControllers.Add((IController)RobotVisualBodyController);

            RobotSpawnController = new RobotSpawnController(this, new List<IController> { (IController)RobotDataController, (IController)RobotVisualBodyController });
            subsystemsControllers.Add((IController)RobotSpawnController);

            RobotMovingController = new RobotMovingController(this, new List<IController> { (IController)RobotVisualBodyController, (IController)RobotSpawnController });
            subsystemsControllers.Add((IController)RobotMovingController);

            RobotAnimationController = new RobotAnimationController(this, new List<IController> { (IController)RobotMovingController });
            subsystemsControllers.Add((IController)RobotAnimationController);

            RobotTakeDamageController = new RobotTakeDamageController(this, new List<IController> { (IController)RobotBuffDebuffController, (IController)RobotDataController });
            subsystemsControllers.Add((IController)RobotTakeDamageController);

            RobotBuffDebuffController = new RobotBuffDebuffController(this, null);
            subsystemsControllers.Add((IController)RobotBuffDebuffController);

            RobotImpactableSensorController = new RobotImpactableSensorController(this, new List<IController> { (IController)RobotVisualBodyController });
            subsystemsControllers.Add((IController)RobotImpactableSensorController);

            RobotAIController = new RobotAIController(this, new List<IController> { (IController)RobotVisualBodyController, (IController)RobotMovingController });
            subsystemsControllers.Add((IController)RobotAIController);

            RobotAwarenesController = new RobotAwarenesController(this, new List<IController> { (IController)RobotVisualBodyController });
            subsystemsControllers.Add((IController)RobotAwarenesController);
        }
    }
}
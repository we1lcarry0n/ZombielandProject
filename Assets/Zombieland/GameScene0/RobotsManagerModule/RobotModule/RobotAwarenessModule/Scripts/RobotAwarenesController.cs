using System;
using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule
{
    public class RobotAwarenesController : Controller, IRobotAwarenesController
    {
        public event Action<IController> OnDeadBodyDetected;

        public IRobotController RobotController { get; private set; }
        public IRobotDeadBodySensorController RobotDeadBodySensorController { get; private set; }


        public RobotAwarenesController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            RobotDeadBodySensorController = new RobotDeadBodySensorController(this, new List<IController> { (IController)RobotController.RobotVisualBodyController});
            subsystemsControllers.Add((IController)RobotDeadBodySensorController);
            RobotDeadBodySensorController.OnDeadBodyDetected += DeadBodyDetected;
        }


        private void DeadBodyDetected(IController controller)
        {
            OnDeadBodyDetected?.Invoke(controller);
        }
    }
}
using System;
using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public class RobotDeadBodySensorController : Controller, IRobotDeadBodySensorController
    {
        public event Action<IController> OnDeadBodyDetected;

        public IRobotAwarenesController RobotAwarenesController { get; private set; }

        private RobotDeadBodySensor _robotDeadBodySensor;


        public RobotDeadBodySensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotAwarenesController = parentController as IRobotAwarenesController;
        }

        public void StartRobotDeadBodySensor()
        {
            _robotDeadBodySensor.StartSensor();
        }
        public void StopRobotDeadBodySensor()
        {
            _robotDeadBodySensor.StopSensor();
        }

        protected override void CreateHelpersScripts()
        {
            switch (RobotAwarenesController.RobotController.RobotDataController.RobotData.RobotType)
            {
                case RobotType.Graber:
                    _robotDeadBodySensor = RobotAwarenesController.RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotDeadBodySensor>();
                    _robotDeadBodySensor.StartSensor();
                    _robotDeadBodySensor.OnDeadBodyDetected += DeadBodyDetected;
                    break;

                default:
                    break;
            }
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void DeadBodyDetected(IController controller)
        {
            OnDeadBodyDetected?.Invoke(controller);
        }
    }
}
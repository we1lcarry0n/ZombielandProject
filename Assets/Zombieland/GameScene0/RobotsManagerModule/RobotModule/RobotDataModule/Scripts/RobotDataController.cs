using System.Collections.Generic;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule
{
    public class RobotDataController : Controller, IRobotDataController
    {
        public RobotData RobotData { get; private set; }
        public IRobotController RobotController { get; private set; }


        public RobotDataController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }

        protected override void CreateHelpersScripts()
        {
            LoadDefaultValue();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void LoadDefaultValue()
        {
            RobotData = RobotController.RobotsManagerController.RootController.GameDataController.GetData<RobotData>(RobotController.RobotSpawnData.RobotJsonFileName);
            RobotData.RobotSpawnData = RobotController.RobotSpawnData;
        }

    }
}
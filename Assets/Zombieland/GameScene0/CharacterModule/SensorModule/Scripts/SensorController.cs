using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class SensorController : Controller, ISensorController
    {
        public ICharacterController CharacterController { get; private set; }
        public IEnvironmentSensorController EnvironmentSensorController { get; private set; }
        public IImpactableSensorController ImpactableSensorController { get; private set; }

        public SensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            EnvironmentSensorController = new EnvironmentSensorController(this, new List<IController> { (IController)CharacterController.EquipmentController });
            subsystemsControllers.Add((IController)EnvironmentSensorController);

            ImpactableSensorController = new ImpactableSensorController(this, new List<IController> { (IController)CharacterController.EquipmentController });
            subsystemsControllers.Add((IController)ImpactableSensorController);
        }
    }
}

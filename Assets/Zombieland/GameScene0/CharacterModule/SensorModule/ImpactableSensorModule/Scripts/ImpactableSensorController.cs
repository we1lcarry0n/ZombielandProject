using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule
{
    public class ImpactableSensorController : Controller, IImpactableSensorController
    {
        public ISensorController SensorController { get; private set; }
        public List<Impactable> Impactables {  get; private set; }

        public ImpactableSensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            SensorController = parentController as ISensorController;
        }

        protected override void CreateHelpersScripts()
        {
            ImpactableInstaller impactableInstaller = new ImpactableInstaller(this);
            Impactables = impactableInstaller.Install();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
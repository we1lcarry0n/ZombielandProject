using System.Collections.Generic;


namespace Zombieland.GameScene0.NPCModule.NPCImpactableSensorModule
{
    public class NPCImpactableSensorController : Controller, INPCImpactableSensorController
    {
        public INPCController NPCController { get; private set; }
        public List<Impactable> Impactables { get; private set; }

        public NPCImpactableSensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
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
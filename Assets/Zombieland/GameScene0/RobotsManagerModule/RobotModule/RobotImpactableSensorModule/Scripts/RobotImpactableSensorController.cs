using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotImpactableSensorModule
{
    public class RobotImpactableSensorController : Controller, IRobotImpactableSensorController
    {
        public IRobotController RobotController { get; private set; }
        public List<Impactable> Impactables { get; private set; }


        public RobotImpactableSensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
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
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAimingModule
{
    public class NPCAimingController : Controller, INPCAimingController
    {
        public INPCController NPCController { get; private set; }

        private Aiming _aiming;

        public NPCAimingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public Transform GetTarget()
        {
            return _aiming.GetTarget();
        }

        protected override void CreateHelpersScripts()
        {
            _aiming = new Aiming(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
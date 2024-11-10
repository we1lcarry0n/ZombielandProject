using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.NPCModule.NPCTakeDamageModule
{
    public class NPCTakeDamageController : Controller, INPCTakeDamageController
    {
        public event Action<Vector3, Vector3> OnApplyImpact;

        public INPCController NPCController { get; private set; }

        private TakerImpact _takerImpact;


        public NPCTakeDamageController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }


        public void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _takerImpact.ApplyImpact(damageTaken);
            OnApplyImpact?.Invoke(impactCollisionPosition, impactDirection);
        }

        protected override void CreateHelpersScripts()
        {
            _takerImpact = new TakerImpact(NPCController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
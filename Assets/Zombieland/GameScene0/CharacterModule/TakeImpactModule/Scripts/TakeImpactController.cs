using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public class TakeImpactController : Controller, ITakeImpactController
    {
        public event Action<Vector3, Vector3> OnApplyImpact;

        public ICharacterController CharacterController { get; private set; }

        private TakerImpact _takerImpact;


        public TakeImpactController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _takerImpact.ApplyImpact(damageTaken);
            OnApplyImpact?.Invoke(impactCollisionPosition, impactDirection);
        }

        protected override void CreateHelpersScripts()
        {
            _takerImpact = new TakerImpact(CharacterController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
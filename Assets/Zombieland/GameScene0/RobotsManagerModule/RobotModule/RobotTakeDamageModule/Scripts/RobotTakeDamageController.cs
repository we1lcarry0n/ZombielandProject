using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotTakeDamageModule
{
    public class RobotTakeDamageController : Controller, IRobotTakeDamageController
    {
        public event Action<Vector3, Vector3> OnApplyImpact;

        public IRobotController RobotController { get; private set; }

        private TakerImpact _takerImpact;


        public RobotTakeDamageController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _takerImpact.ApplyImpact(damageTaken);
            OnApplyImpact?.Invoke(impactCollisionPosition, impactDirection);
        }


        protected override void CreateHelpersScripts()
        {
            _takerImpact = new TakerImpact(RobotController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
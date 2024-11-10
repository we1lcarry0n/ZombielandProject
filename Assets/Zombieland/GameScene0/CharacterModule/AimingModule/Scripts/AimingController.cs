using UnityEngine;
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.AimingModule.Test;

namespace Zombieland.GameScene0.CharacterModule.AimingModule
{
    public class AimingController : Controller, IAimingController
    {
        public ICharacterController CharacterController { get; private set; }
        
        private Aiming _aiming;
        private TargetCaller _targetCaller;

        public AimingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            Cursor.lockState = CursorLockMode.Locked;
        }
        public Transform GetTarget()
        {
            return _aiming.GetTarget();
        }

        public override void Disable()
        {
            _aiming.Disable();

            base.Disable();
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
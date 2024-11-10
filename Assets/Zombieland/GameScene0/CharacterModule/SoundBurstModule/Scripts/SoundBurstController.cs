using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurstController : Controller, ISoundBurstController
    {
        public event Action<IController> OnSound;

        public ICharacterController CharacterController { get; private set; }
        
        private SoundBurst _soundBurst;
        
        public SoundBurstController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }
        
        public override void Disable()
        {
            CharacterController.CharacterWeaponController.OnShotPerformed -= PlayWeaponSound;
            CharacterController.AnimationController.OnStep -= PlayOnStepSound;
            CharacterController.TakeImpactController.OnApplyImpact -= PlayImpactSound;

            base.Disable();
        }
        
        protected override void CreateHelpersScripts()
        {
            _soundBurst = new SoundBurst(this);
            _soundBurst.OnSound += SoundHandler;    

            CharacterController.CharacterWeaponController.OnShotPerformed += PlayWeaponSound;
            CharacterController.AnimationController.OnStep += PlayOnStepSound;
            CharacterController.TakeImpactController.OnApplyImpact += PlayImpactSound;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void SoundHandler()
        { 
            OnSound?.Invoke((IController)CharacterController);
        }

        private void PlayWeaponSound(Weapon weapon)
        {
            _soundBurst.PlaySound(weapon.WeaponData.SoundName);
        }

        private void PlayOnStepSound()
        {
            _soundBurst.PlaySound("Walk");
        }

        private void PlayImpactSound(Vector3 vector1, Vector3 vector2)
        {
            _soundBurst.PlaySound("Hit");
        }
    }
}

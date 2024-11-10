using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class AnimationController : Controller, IAnimationController
    {
        public event Action<Vector3> OnAnimationMove;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        public ICharacterController CharacterController { get; private set; }

        private CharacterAnimator _characterAnimator;
        private CharacterRagdoll _characterRagdoll;


        public AnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            CharacterController.TakeImpactController.OnApplyImpact -= ApplyImpactHandler;

            _characterAnimator.OnAnimationMove -= AnimationMoveHandler;
            _characterAnimator.OnAnimationAttack -= AnimationAttackHandler;
            _characterAnimator.OnAnimationCreateWeapon -= AnimationCreateWeaponHandler;
            _characterAnimator.OnAnimationDestroyWeapon -= AnimationDestroyWeaponHandler;
            _characterAnimator.OnStep -= StepHandler;
            _characterAnimator.Disable();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            CharacterController.TakeImpactController.OnApplyImpact += ApplyImpactHandler;

            _characterAnimator = CharacterController.VisualBodyController.CharacterInScene.AddComponent<CharacterAnimator>();
            _characterAnimator.Init(this);
            _characterAnimator.OnAnimationMove += AnimationMoveHandler;
            _characterAnimator.OnAnimationAttack += AnimationAttackHandler;
            _characterAnimator.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            _characterAnimator.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;
            _characterAnimator.OnStep += StepHandler;

            _characterRagdoll = CharacterController.VisualBodyController.CharacterInScene.AddComponent<CharacterRagdoll>();
            _characterRagdoll.Init(this);

            //Test
            //TestShooter testShooter = CharacterController.VisualBodyController.CharacterInScene.AddComponent<TestShooter>();
            //testShooter.Init(this, _characterRagdoll);
        }

        private void ApplyImpactHandler(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _characterRagdoll.Hit(impactCollisionPosition, impactDirection);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void AnimationMoveHandler(Vector3 deltaPosition)
        {
            OnAnimationMove?.Invoke(deltaPosition);
        }

        private void AnimationAttackHandler(bool isFire)
        {
            OnAnimationAttack?.Invoke(isFire);
        }

        private void AnimationCreateWeaponHandler(string weaponPrefabName)
        {
            OnAnimationCreateWeapon?.Invoke(weaponPrefabName);
        }

        private void AnimationDestroyWeaponHandler()
        {
            OnAnimationDestroyWeapon?.Invoke();
        }

        private void StepHandler()
        {
            OnStep?.Invoke();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCAnimationController : Controller, INPCAnimationController
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        public INPCController NPCController { get; private set; }

        private NPCAnimator _nPCAnimator;
        private NPCRagdoll _nPCRagdoll;

        public NPCAnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public override void Disable()
        {
            NPCController.NPCTakeDamageController.OnApplyImpact -= ApplyImpactHandler;

            _nPCAnimator.OnAnimatorMoveEvent -= OnAnimatorMoveEventHandler;
            _nPCAnimator.OnAnimationAttack -= AnimationAttackHandler;
            _nPCAnimator.OnAnimationCreateWeapon -= AnimationCreateWeaponHandler;
            _nPCAnimator.OnAnimationDestroyWeapon -= AnimationDestroyWeaponHandler;
            _nPCAnimator.OnStep -= StepHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            NPCController.NPCTakeDamageController.OnApplyImpact += ApplyImpactHandler;

            _nPCAnimator = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCAnimator>();
            _nPCAnimator.Init(this);
            _nPCAnimator.OnAnimatorMoveEvent += OnAnimatorMoveEventHandler;
            _nPCAnimator.OnAnimationAttack += AnimationAttackHandler;
            _nPCAnimator.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            _nPCAnimator.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;
            _nPCAnimator.OnStep += StepHandler;

            _nPCRagdoll = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCRagdoll>();
            _nPCRagdoll.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void ApplyImpactHandler(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _nPCRagdoll.Hit(impactCollisionPosition, impactDirection);
        }

        private void OnAnimatorMoveEventHandler(Vector3 animatorRootPosition)
        {
            OnAnimatorMoveEvent?.Invoke(animatorRootPosition);
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
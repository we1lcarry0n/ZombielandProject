using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCWeaponModule
{
    public class NPCWeaponController : Controller, INPCWeaponController
    {
        public event Action<Weapon> OnShotPerformed;
        public event Action OnShotFailed;

        public INPCController NPCController { get; private set; }
        public IWeapon Weapon { get; private set; }
        public Transform WeaponPointFire { get; private set; }

        public NPCWeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public override void Disable()
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Weapon.ShotProcess.OnShotPerformed -= ShotHandler;
            }

            NPCController.NPCEquipmentController.OnWeaponChanged -= WeaponChangedHandler;
            NPCController.NPCVisualBodyController.OnWeaponInSceneReady -= WeaponInSceneReadyHandler;
            NPCController.NPCAnimationController.OnAnimationAttack -= ButtonFireHandler;

            base.Disable();
        }


        protected override void CreateHelpersScripts()
        {
            NPCController.NPCEquipmentController.OnWeaponChanged += WeaponChangedHandler;
            NPCController.NPCVisualBodyController.OnWeaponInSceneReady += WeaponInSceneReadyHandler;
            NPCController.NPCAnimationController.OnAnimationAttack += ButtonFireHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        #region Private
        private void WeaponChangedHandler(Weapon weapon)
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Weapon.ShotProcess.OnShotPerformed -= ShotHandler;
            }

            Weapon = weapon;
        }

        private void WeaponInSceneReadyHandler()
        {
            Weapon.Init(this);
            Weapon.ShotProcess.OnShotPerformed += ShotHandler;
            WeaponPointFire = NPCController.NPCVisualBodyController.WeaponInScene.transform.Find("PointFire");
        }

        private void ButtonFireHandler(bool isFire)
        {
            if (Weapon != null)
            {
                if (isFire)
                {
                    Weapon.ShotProcess.StartFire();
                }
                else
                {
                    Weapon.ShotProcess.StopFire();
                }
            }
        }

        private void ShotHandler()
        {
            if (Weapon != null)
            {
                OnShotPerformed?.Invoke((Weapon)Weapon);
            }
        }
        #endregion
    }
}
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.NPCModule.NPCVFXModule
{
    public class NPCVFXController : Controller, INPCVFXController
    {
        public INPCController NPCController { get; private set; }

        private VFXCreator _vFXCreator;

        public NPCVFXController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
            _vFXCreator = new VFXCreator(this);
        }

        protected override void CreateHelpersScripts()
        {
            NPCController.NPCWeaponController.OnShotPerformed += ShotPerformedHandler;
            NPCController.NPCTakeDamageController.OnApplyImpact += ApplyImpactHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void ShotPerformedHandler(Weapon weapon)
        {
            if (weapon.WeaponData.VFXPrefabName != "")
            {
                _vFXCreator.CtreateVFX(weapon.WeaponData.VFXPrefabName, NPCController.NPCWeaponController.WeaponPointFire.position, NPCController.NPCWeaponController.WeaponPointFire.rotation);
            }
        }

        private void ApplyImpactHandler(Vector3 position, Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction);

            _vFXCreator.CtreateVFX("CFX2_Blood", position, rotation);
        }
    }
}
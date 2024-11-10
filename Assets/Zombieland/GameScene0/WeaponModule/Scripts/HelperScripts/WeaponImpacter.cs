using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterWeaponModule;
using Zombieland.GameScene0.ImpactModule;
using Zombieland.GameScene0.NPCModule.NPCWeaponModule;

namespace Zombieland.GameScene0.WeaponModule
{
    public class WeaponImpacter
    {
        private IController _weaponController;

        public WeaponImpacter(IController weaponController) 
        { 
            _weaponController = weaponController;
        }

        public Impact GetCurrentImpact()
        {
            Impact impact = null;

            if (_weaponController is ICharacterWeaponController characterWeaponController)
            {
                impact = characterWeaponController.CharacterController.RootController.GameDataController.GetData<Impact>(characterWeaponController.CharacterController.EquipmentController.CurrentImpactID);

                impact.ImpactData.ImpactOwner = (IController)characterWeaponController.CharacterController;

                impact.ImpactData.FollowTargetTransform = characterWeaponController.CharacterController.AimingController.GetTarget();

                impact.ImpactData.ObjectSpawnPosition = characterWeaponController.WeaponPointFire.position;

                impact.ImpactData.ObjectParentTransform = characterWeaponController.WeaponPointFire;

                impact.ImpactData.ObjectRotation = AddShotSpread(impact.ImpactData.FollowTargetTransform);
            }

            if (_weaponController is INPCWeaponController nPCWeaponController)
            {
                impact = nPCWeaponController.NPCController.NPCManagerController.RootController.GameDataController.GetData<Impact>(nPCWeaponController.NPCController.NPCEquipmentController.CurrentImpactID);

                impact.ImpactData.ImpactOwner = (IController)nPCWeaponController.NPCController;

                impact.ImpactData.FollowTargetTransform = nPCWeaponController.NPCController.NPCAimingController.GetTarget();

                impact.ImpactData.ObjectSpawnPosition = nPCWeaponController.WeaponPointFire.position;

                impact.ImpactData.ObjectParentTransform = nPCWeaponController.WeaponPointFire;

                impact.ImpactData.ObjectRotation = AddShotSpread(impact.ImpactData.FollowTargetTransform);
            }

            return impact;
        }

        private Quaternion AddShotSpread(Transform target)
        {
            Quaternion finalRotation = new Quaternion();

            if (_weaponController is ICharacterWeaponController characterWeaponController)
            {

                float shotAccuracy = characterWeaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);

                if (characterWeaponController.Weapon.WeaponData.HasTarget)
                {
                    Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);

                    Vector3 startPosition = characterWeaponController.WeaponPointFire.position;

                    Vector3 directionToTarget = (target.position - startPosition).normalized;

                    Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                    finalRotation = deviationRotation * directionQuaternion;
                }
                else
                {
                    finalRotation = characterWeaponController.WeaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
                }
            }

            if (_weaponController is INPCWeaponController nPCrWeaponController)
            {

                float shotAccuracy = nPCrWeaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);

                if (nPCrWeaponController.Weapon.WeaponData.HasTarget)
                {
                    Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);

                    Vector3 startPosition = nPCrWeaponController.WeaponPointFire.position;

                    Vector3 directionToTarget = (target.position - startPosition).normalized;

                    Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                    finalRotation = deviationRotation * directionQuaternion;
                }
                else
                {
                    finalRotation = nPCrWeaponController.WeaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
                }
            }


            return finalRotation;
        }
    }
}
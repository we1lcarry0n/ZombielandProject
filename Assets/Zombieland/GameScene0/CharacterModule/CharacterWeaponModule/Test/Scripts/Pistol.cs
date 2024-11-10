using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.CharacterWeaponModule;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class Pistol
    {
        private Weapon _pistol;
        private ICharacterWeaponController _weaponController;

        public Pistol(ICharacterWeaponController weaponController)
        {
            _pistol = new Weapon();
            _pistol.WeaponData = new WeaponData();
            _weaponController = weaponController;
        }

        public void Init()
        {
            //// Wrench
            //_pistol.WeaponData.ID = "Wrench_0";
            //_pistol.WeaponData.Name = "Wrench";
            //_pistol.WeaponData.PrefabName = "Wrench_0";
            //_pistol.WeaponData.AvailableImpactIDs = new List<string> { "Wrench" };
            //_pistol.WeaponData.ShootCooldown = 0f;
            //_pistol.WeaponData.ShotAccuracy = 0f;
            //_pistol.WeaponData.MaxImpactCount = -1;
            //_pistol.WeaponData.HasTarget = false;

            //// Pistol
            //_pistol.WeaponData.ID = "Pistol_0";
            //_pistol.WeaponData.Name = "Pistol";
            //_pistol.WeaponData.PrefabName = "Pistol_0";
            //_pistol.WeaponData.AvailableImpactIDs = new List<string> { "GunBullet" };
            //_pistol.WeaponData.ShootCooldown = 1f;
            //_pistol.WeaponData.ShotAccuracy = 0.5f;
            //_pistol.WeaponData.MaxImpactCount = 15;
            //_pistol.WeaponData.HasTarget = false;

            //// AK
            //_pistol.WeaponData.ID = "AK_0";
            //_pistol.WeaponData.Name = "AK";
            //_pistol.WeaponData.PrefabName = "AK_0";
            //_pistol.WeaponData.AvailableImpactIDs = new List<string> { "MachineGunBullet" };
            //_pistol.WeaponData.ShootCooldown = 0.2f;
            //_pistol.WeaponData.ShotAccuracy = 0.5f;
            //_pistol.WeaponData.MaxImpactCount = 33;
            //_pistol.WeaponData.HasTarget = false;
        }

        public void Serialize() 
        {
            _weaponController.CharacterController.RootController.GameDataController.SaveDada<Weapon>("Wrench_0", _pistol);
        }
    }
}
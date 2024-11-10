using System;
using Zombieland.GameScene0.CharacterModule.CharacterWeaponModule;
using Zombieland.GameScene0.NPCModule.NPCWeaponModule;

namespace Zombieland.GameScene0.WeaponModule
{
    [Serializable]
    public class Weapon : IWeapon
    {
        public WeaponData WeaponData { get; set; }
        public IShotProcess ShotProcess { get; private set; }

        public void Init(IController weaponController)
        {
            if (weaponController is ICharacterWeaponController characterWeaponController)
            {
                WeaponData.Owner = (IController)characterWeaponController.CharacterController;
            }

            if (weaponController is INPCWeaponController nPCWeaponController)
            {
                WeaponData.Owner = (IController)nPCWeaponController.NPCController;
            }

            ShotProcess = new ShotProcess();
            ShotProcess.Init(weaponController);
        }
    }
}
using System.Collections.Generic;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    public class WeaponSlot
    {
        public Weapon EquippedWeapon;
        public string EquippedImpact;

        public WeaponSlot(Weapon equippedWeapon, string equippedImpact)
        {
            EquippedWeapon = equippedWeapon;
            EquippedImpact = equippedImpact;
        }

        //public WeaponSlot empty { get { return emptySlot; } }

        public void AddEquippedImpact(string impactID)
        {
            EquippedImpact = impactID;
        }

        public void SetEquippedWeapon(Weapon equippedWeapon)
        {
            EquippedWeapon = equippedWeapon;
        }
    }
}
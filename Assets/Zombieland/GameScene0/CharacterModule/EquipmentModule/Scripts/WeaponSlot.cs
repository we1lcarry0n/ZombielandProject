using System.Collections.Generic;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public class WeaponSlot
    {
        public Weapon EquippedWeapon;
        public Dictionary<string, int> EquippedImpacts;

        //static readonly WeaponSlot emptySlot = new WeaponSlot(null, null);

        public WeaponSlot(Weapon equippedWeapon, Dictionary<string, int> equippedImpacts)
        {
            EquippedWeapon = equippedWeapon;
            EquippedImpacts = equippedImpacts;
        }

        //public WeaponSlot empty { get { return emptySlot; } }

        public void AddEquippedImpact(string impactID, int amount)
        {
            EquippedImpacts.Add(impactID, amount);
        }

        public void SetEquippedWeapon(Weapon equippedWeapon)
        {
            EquippedWeapon = equippedWeapon;
        }
    }
}
using System;
using System.Collections.Generic;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public interface IEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnEquipmentChanged;
        event Action OnImpactDepleted;

        //Dictionary<int, WeaponSlot> WeaponSlots { get; }
        List<WeaponSlot> WeaponSlots { get; }
        string CurrentImpactID { get; }
        int CurrentImpactCount { get; set; }
        string CurrentOutfitEquipped { get; }

        ICharacterController CharacterController { get; }
    }
}
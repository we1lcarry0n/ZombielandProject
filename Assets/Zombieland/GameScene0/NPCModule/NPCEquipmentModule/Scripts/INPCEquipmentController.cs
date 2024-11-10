using System;
using System.Collections.Generic;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    public interface INPCEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnEquipmentChanged;
        event Action OnImpactDepleted;

        List<WeaponSlot> WeaponSlots { get; }
        string CurrentImpactID { get; }
        int CurrentImpactCount { get; set;  }
        string CurrentOutfitEquipped { get; }

        void EquipDefaultWeapon(int defaultWeaponIndex);

        INPCController NPCController { get; }
    }
}
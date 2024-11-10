using System.Collections.Generic;
using System;


namespace Zombieland.GameScene0.NPCModule.NPCInventoryModule
{
    public interface INPCInventoryController
    {
        event Action<string, int, string, int> OnMainSlotEquipped;
        event Action<string, int> OnCurrentImpactEquipped;
        event Action<string> OnCurrentOutfitEquipped;

        Dictionary<string, NPCInventoryItem> ItemsInInventory { get; }

        INPCController NPCController { get; }

        void PickUpItem(string itemName, int count);

        void EquipWeaponIntoActiveSlot(string name, int slotNumber, string defaultImpactName, int defaultImpactCount);
        void EquipCurrentImpact(string name, int amount);
    }
}
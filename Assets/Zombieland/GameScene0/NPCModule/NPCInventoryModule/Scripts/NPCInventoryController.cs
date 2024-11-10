using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCInventoryModule
{
    public class NPCInventoryController : Controller, INPCInventoryController
    {
        public event Action<string, int, string, int> OnMainSlotEquipped;
        public event Action<string, int> OnCurrentImpactEquipped;
        public event Action<string> OnCurrentOutfitEquipped;

        public Dictionary<string, NPCInventoryItem> ItemsInInventory { get; private set; }

        public INPCController NPCController { get; private set; }


        public NPCInventoryController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public override void Disable()
        {
            base.Disable();
        }

        public void EquipWeaponIntoActiveSlot(string name, int slotNumber, string defaultImpactName, int defaultImpactCount)
        {
            OnMainSlotEquipped?.Invoke(name, slotNumber, defaultImpactName, defaultImpactCount);
        }

        public void EquipCurrentImpact(string name, int amount)
        {
            OnCurrentImpactEquipped?.Invoke(name, amount);
        }

        public void PickUpItem(string itemName, int count)
        {
            ItemsInInventory.Add("Some ID", new NPCInventoryItem(itemName, count));
            Debug.Log($"Added {count} of {itemName}!");
        }


        protected override void CreateHelpersScripts()
        {
            EquipWeaponIntoActiveSlot("ZombieHand_0", 0, "Wrench", 1);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void EquipCurrentOutfit(string name)
        {
            OnCurrentOutfitEquipped?.Invoke(name);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.InventoryModule
{
    public class InventoryController : Controller, IInventoryController
    {
        public event Action<string, int, int> OnMainSlotEquipped;
        public event Action<string, int> OnCurrentImpactEquipped;
        public event Action<string> OnCurrentOutfitEquipped;

        public Dictionary<string, InventoryItem> ItemsInInventory { get; private set; }

        public ICharacterController CharacterController { get; private set; }

        public InventoryController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void PickUpItem(string itemName, int count)
        {
            ItemsInInventory.Add("Some ID", new InventoryItem(itemName, count));
            Debug.Log($"Added {count} of {itemName}!");
            // For testing purpose try to call Equip here, with an additional parameter (int slotNumber) determined in every temporary weapon prefab on Scene.
        }

        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            GameObject testPickUp = GameObject.Instantiate(new GameObject("TestPickUp"));
            testPickUp.AddComponent<WeaponImpactPicker>();
            testPickUp.GetComponent<WeaponImpactPicker>().Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED

        public void EquipWeaponIntoActiveSlot(string name, int slotNumber, int defaultImpactCount)
        {
            OnMainSlotEquipped?.Invoke(name, slotNumber, defaultImpactCount);
            // This method should be subscribed to DragNDrop Event in UIEquipment when something is dragged into Weapon Slot.
        }

        public void EquipCurrentImpact(string name, int amount)
        {
            OnCurrentImpactEquipped?.Invoke(name, amount);
            // This method should be subscribed to OnClick Event in UIEquipment when impact is selected from Inventory somehow.
        }

        private void EquipCurrentOutfit(string name)
        {
            OnCurrentOutfitEquipped?.Invoke(name);
            // This method should be subscribed to DragNDrop Event in UIEquipment when something is dragged into Outfit Slot.
        }
    }
}


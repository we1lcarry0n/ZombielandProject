using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    public class NPCEquipmentController : Controller, INPCEquipmentController
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action<string> OnEquipmentChanged;
        public event Action OnImpactDepleted;

        public INPCController NPCController { get; private set; }
        public List<WeaponSlot> WeaponSlots { get; private set; }
        public string CurrentImpactID { get; private set; }
        public int CurrentImpactCount
        {
            get { return _currentImpactCount; }
            set { if (value <= 0) return; else _currentImpactCount = value; }
        }
        public string CurrentOutfitEquipped { get; private set; }

        private Weapon _currentWeaponEquipped;
        private int _currentActiveSlotIndex; // For the Future SaveSystem
        private int _currentImpactCount;

        #region PUBLIC
        public NPCEquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
            WeaponSlots = new List<WeaponSlot>(4) { null, null, null, null };
            CurrentImpactCount = Int32.MaxValue;
        }

        public override void Disable()
        {
            NPCController.NPCAIController.OnSlotNumber1 -= Number1Handler;
            NPCController.NPCAIController.OnSlotNumber2 -= Number2Handler;
            NPCController.NPCAIController.OnSlotNumber3 -= Number3Handler;
            NPCController.NPCAIController.OnSlotNumber4 -= Number4Handler;

            base.Disable();
        }

        public void EquipDefaultWeapon(int defaultWeaponIndex)
        {
            switch (defaultWeaponIndex)
            {
                case 1:
                    Number1Handler();
                    break;
                case 2:
                    Number2Handler();
                    break;
                case 3:
                    Number3Handler();
                    break;
                case 4:
                    Number4Handler();
                    break;
            }
        }
        #endregion PUBLIC

        protected override void CreateHelpersScripts()
        {
            NPCController.NPCAIController.OnSlotNumber1 += Number1Handler;
            NPCController.NPCAIController.OnSlotNumber2 += Number2Handler;
            NPCController.NPCAIController.OnSlotNumber3 += Number3Handler;
            NPCController.NPCAIController.OnSlotNumber4 += Number4Handler;

            foreach (NPCEquipmentSlotData slotData in NPCController.NPCDataController.NPCData.NPCEquipmentSlotDatas)
            {
                FillSlot(slotData.WeaponJSONName, slotData.SlotNumber, slotData.DefaultImpactIndexInList);
            }
        }

        #region PROTECTED
        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void FillSlot(string weaponJSONName, int slotNumber, int defaultImpactIndex)
        {
            Weapon weapon = NPCController.NPCManagerController.RootController.GameDataController.GetData<Weapon>(weaponJSONName);
            WeaponSlots[slotNumber] = new WeaponSlot(weapon, weapon.WeaponData.AvailableImpactIDs[defaultImpactIndex]);
        }
        #endregion PROTECTED

        #region PRIVATE
        private void Number1Handler()
        {
            Debug.Log("Number1Handler");

            if (WeaponSlots[0] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[0].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[0].EquippedWeapon;
                CurrentImpactID = WeaponSlots[0].EquippedImpact;
                _currentActiveSlotIndex = 0;
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[0].EquippedWeapon.WeaponData.Name} is equipped for {NPCController.NPCDataController.NPCData.Name}!");
                return;
            }
        }

        private void Number2Handler()
        {
            if (WeaponSlots[1] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[1].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[1].EquippedWeapon;
                CurrentImpactID = WeaponSlots[1].EquippedImpact;
                _currentActiveSlotIndex = 1;
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[1].EquippedWeapon.WeaponData.Name} is equipped for {NPCController.NPCDataController.NPCData.Name}!");
                return;
            }
        }

        private void Number3Handler()
        {
            if (WeaponSlots[2] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[2].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[2].EquippedWeapon;
                CurrentImpactID = WeaponSlots[2].EquippedImpact;
                _currentActiveSlotIndex = 2;
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[2].EquippedWeapon.WeaponData.Name} is equipped for {NPCController.NPCDataController.NPCData.Name}!");
                return;
            }
        }

        private void Number4Handler()
        {
            if (WeaponSlots[3] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[3].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[3].EquippedWeapon;
                CurrentImpactID = WeaponSlots[3].EquippedImpact;
                _currentActiveSlotIndex = 3;
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[3].EquippedWeapon.WeaponData.Name} is equipped for {NPCController.NPCDataController.NPCData.Name}!");
                return;
            }
        }
        #endregion PRIVATE
    }
}
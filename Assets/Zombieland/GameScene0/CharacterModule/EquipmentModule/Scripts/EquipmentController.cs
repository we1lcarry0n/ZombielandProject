using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public class EquipmentController : Controller, IEquipmentController
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action<string> OnEquipmentChanged;
        public event Action OnImpactDepleted;

        public ICharacterController CharacterController { get; private set; }
        //public Dictionary<int, WeaponSlot> WeaponSlots { get; private set; }
        public List<WeaponSlot> WeaponSlots { get; private set; }
        public string CurrentImpactID { get; private set; }
        public int CurrentImpactCount 
        { 
            get { return _currentImpactCount; } 
            set { if (value <= 0) return; else _currentImpactCount = value; }
        }
        public string CurrentOutfitEquipped { get; private set; }

        private Weapon _currentWeaponEquipped;
        private int _currentActiveSlotIndex;
        private int _currentImpactCount;


        public EquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            //WeaponSlots = new Dictionary<int, WeaponSlot>() { { 1, null }, { 2, null }, { 3, null }, { 4, null } };
            WeaponSlots = new List<WeaponSlot>(4) { null, null, null, null};
        }

        public override void Disable()
        {
            CharacterController.RootController.UIController.OnNumber1 -= Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 -= Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 -= Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 -= Number4Handler;
            CharacterController.RootController.UIController.OnWeaponReaload -= ReloadCurrentWeapon;

            CharacterController.InventoryController.OnMainSlotEquipped -= MainSlotEquippedHandler;
            CharacterController.InventoryController.OnCurrentImpactEquipped -= CurrentImpactEquippedHandler;
            CharacterController.InventoryController.OnCurrentOutfitEquipped -= CurrentOutfitEquippedHandler;

            base.Disable();
        }

        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 += Number4Handler;
            CharacterController.RootController.UIController.OnWeaponReaload += ReloadCurrentWeapon;

            CharacterController.InventoryController.OnMainSlotEquipped += MainSlotEquippedHandler;
            CharacterController.InventoryController.OnCurrentImpactEquipped += CurrentImpactEquippedHandler;
            CharacterController.InventoryController.OnCurrentOutfitEquipped += CurrentOutfitEquippedHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED

        #region PRIVATE
        private void MainSlotEquippedHandler(string name, int slotNumber, int defaultImpactCount)
        {
            Debug.Log($"Received {name} into {slotNumber}");
            Weapon weapon = CharacterController.RootController.GameDataController.GetData<Weapon>(name);
            WeaponSlots[slotNumber] = new WeaponSlot(weapon, new Dictionary<string, int>());
            WeaponSlots[slotNumber].EquippedImpacts.Add(weapon.WeaponData.AvailableImpactIDs[0], defaultImpactCount);
            WeaponSlots[slotNumber].SetEquippedWeapon(weapon);
        }

        private void CurrentImpactEquippedHandler(string impactID, int amount)
        {
            if (_currentWeaponEquipped.WeaponData.AvailableImpactIDs.Contains(impactID))
            {
                if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts.ContainsKey(impactID))
                {
                    WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[impactID] += amount;
                    return;
                }
                WeaponSlots[_currentActiveSlotIndex].AddEquippedImpact(impactID, amount);
                CurrentImpactID = impactID; //Temporary logic, will be replaced after proper UI ability to choose impacts
                ReloadCurrentWeapon(); //Temporary logic, will be replaced after proper UI ability to choose impacts
                return;
            }
            Debug.Log($"{impactID} can not be used with {_currentWeaponEquipped.WeaponData.Name}!");
        }

        //probably CurrentImpactSelectedHandler(string impactID) is needed, subscribed to some UIMain event and telling us which Impacts do we want to use.

        private void CurrentOutfitEquippedHandler(string name)
        {
            OnEquipmentChanged?.Invoke(name);
        }

        private void Number1Handler()
        {
            if (WeaponSlots[0] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[0].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[0].EquippedWeapon;
                CurrentImpactID = WeaponSlots[0].EquippedImpacts.Keys.First();
                CurrentImpactCount = WeaponSlots[0].EquippedImpacts[CurrentImpactID];
                _currentActiveSlotIndex = 0;
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[0].EquippedWeapon.WeaponData.Name} is equipped! Shooting with {CurrentImpactID}");
                Debug.Log($"Impacts left: {_currentImpactCount}");
                return;
            }
            Debug.Log("Weapon slot 1 is empty!");
        }

        private void Number2Handler()
        {
            if (WeaponSlots[1] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[1].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[1].EquippedWeapon;
                CurrentImpactID = WeaponSlots[1].EquippedImpacts.Keys.First();
                CurrentImpactCount = WeaponSlots[1].EquippedImpacts[CurrentImpactID];
                _currentActiveSlotIndex = 1;
                Debug.Log($"Weapon in slot 2 : {WeaponSlots[1].EquippedWeapon.WeaponData.Name} is equipped! Shooting with {CurrentImpactID}");
                Debug.Log($"Impacts left: {_currentImpactCount}");
                return;
            }
            Debug.Log("Weapon slot 2 is empty!");
        }

        private void Number3Handler()
        {
            if (WeaponSlots[2] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[2].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[2].EquippedWeapon;
                CurrentImpactID = WeaponSlots[2].EquippedImpacts.Keys.First();
                CurrentImpactCount = WeaponSlots[2].EquippedImpacts[CurrentImpactID];
                _currentActiveSlotIndex = 2;
                Debug.Log($"Weapon in slot 3 : {WeaponSlots[2].EquippedWeapon.WeaponData.Name} is equipped! Shooting with  {CurrentImpactID}");
                Debug.Log($"Impacts left: {_currentImpactCount}");
                return;
            }
            Debug.Log("Weapon slot 3 is empty!");
        }

        private void Number4Handler()
        {
            if (WeaponSlots[3] != null)
            {
                OnWeaponChanged?.Invoke(WeaponSlots[3].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[3].EquippedWeapon;
                CurrentImpactID = WeaponSlots[3].EquippedImpacts.Keys.First();
                CurrentImpactCount = WeaponSlots[0].EquippedImpacts[CurrentImpactID];
                _currentActiveSlotIndex = 3;
                Debug.Log($"Weapon in slot 4 : {WeaponSlots[3].EquippedWeapon.WeaponData.Name} is equipped! Shooting with  {CurrentImpactID}");
                Debug.Log($"Impacts left: {_currentImpactCount}");
                return;
            }
            Debug.Log("Weapon slot 4 is empty!");
        }

        private void ReloadCurrentWeapon()
        {
            if (_currentWeaponEquipped.WeaponData.MaxImpactCount == -1)
            {
                return;
            }
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID] == 0)
            {
                return;
            }
            int reloadAmount = _currentWeaponEquipped.WeaponData.MaxImpactCount - CurrentImpactCount;
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID] == reloadAmount)
            {
                return;
            }
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID] > reloadAmount)
            {
                CurrentImpactCount += reloadAmount;
                WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID] -= reloadAmount;
            }
            else
            {
                CurrentImpactCount += WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID];
                WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[CurrentImpactID] = 0;
            }
        }
        #endregion PRIVATE
    }
}
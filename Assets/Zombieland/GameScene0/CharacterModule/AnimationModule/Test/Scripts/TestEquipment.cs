using System;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestEquipment
    {
        public event Action<Weapon> OnWeaponChanged;

        private IAnimationController _animatorController;

        public TestEquipment(IAnimationController animationController)
        {
            _animatorController = animationController;

            _animatorController.CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            _animatorController.CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            _animatorController.CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
        }

        private void Number1Handler()
        {
            Debug.Log("TestEquipment Number1Handler");

            Weapon weapon = new Weapon();
            weapon.WeaponData = new WeaponData();
            weapon.WeaponData.Name = "Wrench";
            
            OnWeaponChanged?.Invoke(weapon);
        }

        private void Number2Handler()
        {
            Debug.Log("TestEquipment Number2Handler");

            Weapon weapon = new Weapon();
            weapon.WeaponData = new WeaponData();
            weapon.WeaponData.Name = "Pistol";

            OnWeaponChanged?.Invoke(weapon);
        }

        private void Number3Handler()
        {
            Debug.Log("TestEquipment Number3Handler");

            Weapon weapon = new Weapon();
            weapon.WeaponData = new WeaponData();
            weapon.WeaponData.Name = "Shotgun";

            OnWeaponChanged?.Invoke(weapon);
        }
    }
}
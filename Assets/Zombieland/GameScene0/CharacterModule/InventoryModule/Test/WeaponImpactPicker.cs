using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.CharacterModule.InventoryModule;

public class WeaponImpactPicker : MonoBehaviour
{
    private IInventoryController _inventoryController;
    public void Init(IController inventoryController)
    {
        _inventoryController = inventoryController as IInventoryController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { _inventoryController.EquipWeaponIntoActiveSlot("Wrench_0", 0, 1); }
        if (Input.GetKeyDown(KeyCode.P)) { _inventoryController.EquipWeaponIntoActiveSlot("Pistol_0", 1, 20); }
        if (Input.GetKeyDown(KeyCode.P)) { _inventoryController.EquipWeaponIntoActiveSlot("AK_0", 2, 40); }

        if (Input.GetKeyDown(KeyCode.Alpha9)) { _inventoryController.EquipCurrentImpact("GunBullet", 20); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { _inventoryController.EquipCurrentImpact("MachineGunBullet", 40); }
    }
}

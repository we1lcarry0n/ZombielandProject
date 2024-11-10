using UnityEngine;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.WeaponModule;

public class InterractableEquipmentObject : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    private IEquipmentController _equipmentController;

    public void Init(IEquipmentController equipmentController)
    {
        _equipmentController = equipmentController;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void PickedUp()
    {
        //_equipmentController.PickUpWeapon(_weapon);
    }
}

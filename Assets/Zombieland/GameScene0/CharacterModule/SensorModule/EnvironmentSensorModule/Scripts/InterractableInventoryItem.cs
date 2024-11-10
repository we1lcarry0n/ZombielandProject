using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class InterractableInventoryItem : MonoBehaviour, IInterractable
    {
        public IController Controller {get; private set;}

        [SerializeField] private bool _isWeapon; //Temporary filed, while Inventroy Items ID is not ready
        [SerializeField] private string _weaponName; //Temporary filed, while Inventroy Items ID is not ready
        [SerializeField] private int _slotNumberPlaced; //Temporary filed, while Inventroy Items ID is not ready
        [SerializeField] private string _impactID; //Temporary filed, while Inventroy Items ID is not ready
        [SerializeField] private int _impactCount; //Temporary filed, while Inventroy Items ID is not ready

        private bool _isInInterractionRange;

        public bool TryInterract(IEnvironmentSensorController environmentSensorController)
        {
            if (!_isInInterractionRange)
            {
                return false;
            }
            if (_isWeapon)
            {
                environmentSensorController.SensorController.CharacterController.InventoryController.EquipWeaponIntoActiveSlot(_weaponName, _slotNumberPlaced, _impactCount);
            }
            else
            {
                environmentSensorController.SensorController.CharacterController.InventoryController.EquipCurrentImpact(_impactID, _impactCount);
            }
            environmentSensorController.ExcludeFromInterractions(this);
            Destroy(gameObject);
            return true;
        }

        public void ToggleInterractable(bool isInRange)
        {
            _isInInterractionRange = isInRange;
        }
    }
}


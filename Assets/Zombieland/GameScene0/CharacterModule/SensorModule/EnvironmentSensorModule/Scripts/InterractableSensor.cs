using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class InterractableSensor : MonoBehaviour
    {
        private List<IInterractable> _interractablesInRange;
        private IEnvironmentSensorController _environmentSensorController;

        public void Init(IController parentController)
        {
            _interractablesInRange = new List<IInterractable>();
            _environmentSensorController = parentController as IEnvironmentSensorController;
        }

        public void TryInterract()
        {
            if ( _interractablesInRange.Count == 0)
            {
                return;
            }
            /*Debug.Log("The List is not empty");
            if (_interractablesInRange[0] == null)
            {
                Debug.Log("The first element in the list is null");
                _interractablesInRange.RemoveAt(0);
            }
            Debug.Log("The first element was not null and is present");
            if (_interractablesInRange.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }*/
            if (_interractablesInRange[0].TryInterract(_environmentSensorController))
            {
                return;
            }
        }

        public void RemoveInterractable(IInterractable interractable)
        {
            _interractablesInRange.Remove(interractable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(true);
                _interractablesInRange.Add(interractable);
                _environmentSensorController.InterractionTriggerEnter(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInterractable>(out var interractable))
            {
                interractable.ToggleInterractable(false);
                _interractablesInRange.Remove(interractable);
                _environmentSensorController.InterractionTriggerEnter(false);
                // Add logic for proccessing being inside multiple triggers at once
            }
        }
    }
}


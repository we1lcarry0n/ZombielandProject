using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule
{
    public class ImpactableInstaller
    {
        private ImpactableSensorController _impactableSensorController;
        
        public ImpactableInstaller(ImpactableSensorController impactableSensorController) 
        {
            _impactableSensorController = impactableSensorController;
        }

        public List<Impactable> Install() 
        {
            List<Impactable> impactables = new List<Impactable>();

            List<GameObject> sensorTriggersGameobject = _impactableSensorController.SensorController.CharacterController.VisualBodyController.SensorTriggersGameobject;

            if (sensorTriggersGameobject.Count > 0)
            {
                for (int i = 0; i < sensorTriggersGameobject.Count; i++)
                {
                    Impactable impactable = sensorTriggersGameobject[i].AddComponent<Impactable>();
                    impactable.Init((IController) _impactableSensorController.SensorController.CharacterController);
                    impactables.Add(impactable);
                }
            }

            return impactables;
        }
    }
}
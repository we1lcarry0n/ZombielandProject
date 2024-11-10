using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCImpactableSensorModule
{
    public class ImpactableInstaller
    {
        private NPCImpactableSensorController _nPCImpactableSensorController;
        
        public ImpactableInstaller(NPCImpactableSensorController nPCImpactableSensorController) 
        {
            _nPCImpactableSensorController = nPCImpactableSensorController;
        }

        public List<Impactable> Install() 
        {
            List<Impactable> impactables = new List<Impactable>();

            List<GameObject> sensorTriggersGameobject = _nPCImpactableSensorController.NPCController.NPCVisualBodyController.SensorTriggersGameobject;

            if (sensorTriggersGameobject.Count > 0)
            {
                for (int i = 0; i < sensorTriggersGameobject.Count; i++)
                {
                    Impactable impactable = sensorTriggersGameobject[i].AddComponent<Impactable>();
                    impactable.Init((IController)_nPCImpactableSensorController.NPCController);
                    impactables.Add(impactable);
                }
            }

            return impactables;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotImpactableSensorModule
{
    public class ImpactableInstaller
    {
        private RobotImpactableSensorController _robotImpactableSensorController;


        public ImpactableInstaller(RobotImpactableSensorController robotImpactableSensorController)
        {
            _robotImpactableSensorController = robotImpactableSensorController;
        }

        public List<Impactable> Install()
        {
            List<Impactable> impactables = new List<Impactable>();

            List<GameObject> sensorTriggersGameobject = _robotImpactableSensorController.RobotController.RobotVisualBodyController.SensorTriggersGameobject;

            if (sensorTriggersGameobject.Count > 0)
            {
                for (int i = 0; i < sensorTriggersGameobject.Count; i++)
                {
                    Impactable impactable = sensorTriggersGameobject[i].AddComponent<Impactable>();
                    impactable.Init((IController)_robotImpactableSensorController.RobotController);
                    impactables.Add(impactable);
                }
            }

            return impactables;
        }
    }
}
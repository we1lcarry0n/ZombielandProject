using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule
{
    public class GertterTriggers
    {
        private readonly IRobotVisualBodyController _robotVisualBodyController;


        public GertterTriggers(IRobotVisualBodyController robotVisualBodyController)
        {
            _robotVisualBodyController = robotVisualBodyController;
        }

        public List<GameObject> GetSensorTriggers()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            if (_robotVisualBodyController.RobotInScene != null)
            {
                Transform[] childTransforms = _robotVisualBodyController.RobotInScene.GetComponentsInChildren<Transform>();

                foreach (Transform child in childTransforms)
                {
                    Collider collider = child.GetComponent<Collider>();
                    if (collider != null && collider.isTrigger)
                    {
                        CharacterJoint characterJoint = child.GetComponent<CharacterJoint>();
                        if (characterJoint != null)
                        {
                            gameObjects.Add(child.gameObject);
                        }
                    }
                }
            }

            return gameObjects;
        }
    }
}
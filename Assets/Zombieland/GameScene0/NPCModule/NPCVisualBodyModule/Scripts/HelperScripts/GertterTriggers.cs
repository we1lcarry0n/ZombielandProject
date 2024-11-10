using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class GertterTriggers
    {
        private readonly INPCVisualBodyController _nPCVisualBodyController;

        public GertterTriggers(INPCVisualBodyController nPCVisualBodyController)
        {
            _nPCVisualBodyController = nPCVisualBodyController;
        }

        public List<GameObject> GetSensorTriggers()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            if (_nPCVisualBodyController.NPCInScene != null)
            {
                Transform[] childTransforms = _nPCVisualBodyController.NPCInScene.GetComponentsInChildren<Transform>();

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
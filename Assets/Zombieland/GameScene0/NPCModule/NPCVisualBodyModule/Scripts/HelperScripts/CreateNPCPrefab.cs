using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class CreateNPCPrefab
    { 
        public GameObject CreateNPC(INPCVisualBodyController nPCVisualBodyController, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(nPCVisualBodyController.NPCController.NPCDataController.NPCData.PrefabName);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }

        public void Destroy(GameObject characterInScene)
        {
            GameObject.Destroy(characterInScene);
        }
    }
}
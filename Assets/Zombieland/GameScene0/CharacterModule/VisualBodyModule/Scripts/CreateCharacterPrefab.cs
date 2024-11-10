using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateCharacterPrefab
    {
        private const string CHARACTER_PREFAB_NAME = "Character_0_IK";

        public GameObject CreateCharacter(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(CHARACTER_PREFAB_NAME);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }

        public void Destroy(GameObject characterInScene)
        {
            GameObject.Destroy(characterInScene);
        }
    }
}
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class CreateWeaponPrefab
    {
        public GameObject CtreateWeapon(string weaponPrefabName, Transform characterWeaponPoint)
        {
            GameObject prefab = Resources.Load<GameObject>(weaponPrefabName);

            return GameObject.Instantiate(prefab, characterWeaponPoint);
        }

        public void Destroy(GameObject weaponInScene)
        {
            GameObject.Destroy(weaponInScene);
        }
    }
}
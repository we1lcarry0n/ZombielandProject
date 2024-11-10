using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule
{
    public class CreateRobotPrefab
    {
        public GameObject CreateRobot(IRobotVisualBodyController robotVisualBodyController, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            GameObject prefab = Resources.Load<GameObject>(robotVisualBodyController.RobotController.RobotDataController.RobotData.PrefabName);

            return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }

        public void Destroy(GameObject robotInScene)
        {
            GameObject.Destroy(robotInScene);
        }
    }
}
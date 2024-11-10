using UnityEngine;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class CreatorNavMeshSurface
    {
        private const string PREFAB_NPC_NAME = "NavMeshSurfaceLevel1";
        private const string PREFAB_ROBOT_NAME = "NavMeshRobotSurfaceLevel1";

        private GameObject _gameObjectNavMeshNPCSurface;
        private GameObject _gameObjectNavMeshRobotSurface;

        public CreatorNavMeshSurface()
        {
            GameObject prefabNPCNavMesh = Resources.Load<GameObject>(PREFAB_NPC_NAME);
            GameObject prefabRobotNavMesh = Resources.Load<GameObject>(PREFAB_ROBOT_NAME);

            _gameObjectNavMeshNPCSurface = GameObject.Instantiate(prefabNPCNavMesh);
            _gameObjectNavMeshRobotSurface = GameObject.Instantiate(prefabRobotNavMesh);
        }

        public void Destroy()
        {
            GameObject.Destroy(_gameObjectNavMeshNPCSurface);
            GameObject.Destroy(_gameObjectNavMeshRobotSurface);
        }
    }
}
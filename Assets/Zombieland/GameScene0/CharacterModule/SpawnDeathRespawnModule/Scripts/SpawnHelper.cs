using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnHelper
    {
        public event Action<Vector3, Quaternion> OnSpawn;

        private readonly SpawnDeathRespawnController _spawnDeathRespawnController;

        public SpawnHelper(SpawnDeathRespawnController spawnDeathRespawnController)
        {
            _spawnDeathRespawnController = spawnDeathRespawnController;
        }

        public void Start()
        {
            var characterData = _spawnDeathRespawnController.CharacterController.CharacterDataController.CharacterData;
            var radiusAgent = _spawnDeathRespawnController.CharacterController.VisualBodyController.CharacterInScene.GetComponent<UnityEngine.CharacterController>().radius;

            Vector3 defaultPosition = new Vector3();
            Quaternion defaultRotation = new Quaternion();

            SpawnData spawnData = characterData.SpawnData;
            switch (_spawnDeathRespawnController.CharacterController.RootController.EnvironmentController.CurrentLevelName)
            {
                case ("Level1"):
                    defaultPosition = new Vector3(spawnData.Level1DefaultPosition.X, spawnData.Level1DefaultPosition.Y, spawnData.Level1DefaultPosition.Z);
                    defaultRotation = Quaternion.Euler(new Vector3(spawnData.Level1DefaultRotation.X, spawnData.Level1DefaultRotation.Y, spawnData.Level1DefaultRotation.Z));
                    break;

                default:
                    break;
            }
            
            AvailablePosition availablePosition = new AvailablePosition();
            Vector3 spawnPosition = availablePosition.GetSpawnPosition(defaultPosition, spawnData.SpawnRadius, radiusAgent, spawnData.SpawnType);

            if (spawnPosition != null)
            {
                OnSpawn?.Invoke(spawnPosition, defaultRotation);
            }
        }
    }
}
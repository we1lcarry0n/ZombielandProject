using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnDeathRespawnController : Controller, ISpawnDeathRespawnController
    {
        public event Action<Vector3, Quaternion> OnSpawn;

        public ICharacterController CharacterController { get; }

        private SpawnHelper _spawnHelper;

        public SpawnDeathRespawnController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            _spawnHelper = new SpawnHelper(this);
        }

        public void SpawnCharacter()
        {
            _spawnHelper.Start();
        }

        public override void Disable()
        {
            // Викликати метод, який буде зберігати нашу теперішню точку.

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _spawnHelper.OnSpawn += SpawnHandler;
            SpawnCharacter();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void SpawnHandler(Vector3 spawnPosition, Quaternion spawnQuaternion)
        {
            OnSpawn?.Invoke(spawnPosition, spawnQuaternion);
            CharacterController.VisualBodyController.CharacterInScene.transform.position = spawnPosition;
            CharacterController.VisualBodyController.CharacterInScene.transform.rotation = spawnQuaternion;
            //CharacterController.VisualBodyController.CharacterCameraFollow.parent = null;
            CharacterController.VisualBodyController.CharacterInScene.SetActive(true);
        }
    }
}
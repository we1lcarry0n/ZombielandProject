using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotSpawnModule
{
    public class RobotSpawnController : Controller, IRobotSpawnController
    {
        public event Action<Vector3, Quaternion> OnSpawn;

        public IRobotController RobotController { get; private set; }


        public RobotSpawnController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }


        protected override void CreateHelpersScripts()
        {
            ActivateRobot();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void ActivateRobot()
        {
            System.Numerics.Vector3 spawnposition = RobotController.RobotDataController.RobotData.RobotSpawnData.SpawnPosition;
            RobotController.RobotVisualBodyController.RobotInScene.transform.position = new Vector3(spawnposition.X, spawnposition.Y, spawnposition.Z);
            RobotController.RobotVisualBodyController.RobotInScene.SetActive(true);
            OnSpawn?.Invoke(new Vector3(spawnposition.X, spawnposition.Y, spawnposition.Z), Quaternion.identity);
        }
    }
}
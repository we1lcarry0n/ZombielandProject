using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NPCSpawnController : Controller, INPCSpawnController
    {
        public event Action<Vector3, Quaternion> OnSpawn;

        public INPCController NPCController { get; private set; }


        public NPCSpawnController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        protected override void CreateHelpersScripts()
        {
            ActivateNpc();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void ActivateNpc()
        {
            System.Numerics.Vector3 spawnposition = NPCController.NPCDataController.NPCData.NPCSpawnData.SpawnPosition;
            NPCController.NPCVisualBodyController.NPCInScene.transform.position = new Vector3(spawnposition.X, spawnposition.Y, spawnposition.Z);
            NPCController.NPCVisualBodyController.NPCInScene.SetActive(true);
            OnSpawn?.Invoke(new Vector3(spawnposition.X, spawnposition.Y, spawnposition.Z), Quaternion.identity);
        }
    }
}
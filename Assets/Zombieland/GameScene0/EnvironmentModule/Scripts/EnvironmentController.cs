using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;


namespace Zombieland.GameScene0.EnvironmentModule
{
    public class EnvironmentController : Controller, IEnvironmentController
    {
        public event Action<List<AudioSourceObject>> OnLoadedSoundGameobjects;

        public string CurrentLevelName { get; private set; }

        private IRootController _rootController;

        private InitializerEnvironment _initializerEnvironment;
        private CreatorNavMeshSurface _creatorNavMeshSurface;


        public EnvironmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
            _initializerEnvironment = new InitializerEnvironment();
        }

        public override void Disable()
        {
            _initializerEnvironment.OnSceneLoaded += SceneLoadedHandler;
            _creatorNavMeshSurface.Destroy();

            base.Disable();
        }


        protected override void CreateHelpersScripts()
        {
            EnvironmentData environmentData = _rootController.GameDataController.GetData<EnvironmentData>("EnvironmentData");
            CurrentLevelName = environmentData.CurrentLevelName;
            _initializerEnvironment.Init(environmentData);
            _initializerEnvironment.OnSceneLoaded += SceneLoadedHandler;

            _creatorNavMeshSurface = new CreatorNavMeshSurface();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void SceneLoadedHandler()
        {
            List<AudioSourceObject> AudioSourceObjects = GameObject.Find("AllLevel").GetComponent<AudioGameobjectInScene>().AudioSourceObjects;

            OnLoadedSoundGameobjects?.Invoke(AudioSourceObjects);
        }
    }
}
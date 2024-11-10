using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GameDataModule
{
    public class GameDataController : Controller, IGameDataController
    {
        public IRootController RootController { get; }

        private IStorage _storage;

        public GameDataController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
        }

        public void SaveDada<T>(string name, T data)
        {
            _storage.SaveDada(name, data);
        }

        public T GetData<T>(string name)
        {
            return _storage.GetData<T>(name);
        }


        protected override void CreateHelpersScripts()
        {
//#if PLATFORM_STANDALONE_WIN
            _storage = new ResourcesStorage();
//#else
//          _storage = new PlayerPrefsStorage();
//#endif
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
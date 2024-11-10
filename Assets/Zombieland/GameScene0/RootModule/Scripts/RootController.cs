using System.Collections.Generic;
using Zombieland.GameScene0.CameraModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;
using Zombieland.GameScene0.GlobalSoundModule;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.RobotsManagerModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.RootModule
{
    public class RootController : Controller, IRootController
    {
        public ICharacterController CharacterController { get; private set; }
        public IGameDataController GameDataController { get; private set; }
        public IEnvironmentController EnvironmentController { get; private set; }
        public IUIController UIController { get; private set; }
        public ICameraController CameraController { get; private set; }
        public INPCManagerController NPCManagerController { get; private set; }
        public IGlobalSoundController GlobalSoundController { get; private set; }
        public IRobotsManagerController RobotsManagerController { get; private set; }

        public RootController(IController parentController, List<IController> requiredControllers) : base(
            parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterController = new CharacterController(this, new List<IController> {(IController) EnvironmentController, (IController) GameDataController, (IController) UIController});
            GameDataController = new GameDataController(this, null);
            EnvironmentController = new EnvironmentController(this, new List<IController> {(IController) GameDataController});
            UIController = new UIController(this, null);
            CameraController = new CameraController(this, new List<IController> {(IController)CharacterController});
            NPCManagerController = new NPCManagerController(this, new List<IController> { (IController)EnvironmentController });
            GlobalSoundController = new GlobalSoundController(this, new List<IController> { (IController)EnvironmentController, (IController)CharacterController, (IController)NPCManagerController });
            RobotsManagerController = new RobotsManagerController(this, new List<IController> { (IController)EnvironmentController });

            subsystemsControllers.Add((IController) CharacterController);
            subsystemsControllers.Add((IController) GameDataController);
            subsystemsControllers.Add((IController) EnvironmentController);
            subsystemsControllers.Add((IController) UIController);
            subsystemsControllers.Add((IController) CameraController);
            subsystemsControllers.Add((IController) NPCManagerController);
            subsystemsControllers.Add((IController) GlobalSoundController);
            subsystemsControllers.Add((IController) RobotsManagerController);
        }
    }
}
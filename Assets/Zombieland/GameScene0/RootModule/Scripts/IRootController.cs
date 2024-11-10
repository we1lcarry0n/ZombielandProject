using Zombieland.GameScene0.CameraModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;
using Zombieland.GameScene0.UIModule;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.GlobalSoundModule;
using Zombieland.GameScene0.RobotsManagerModule;

namespace Zombieland.GameScene0.RootModule
{
    public interface IRootController
    {
        //TODO : Add required subsystems here
        ICharacterController CharacterController { get; }
        IGameDataController GameDataController { get; }
        IEnvironmentController EnvironmentController { get; }
        IUIController UIController { get; }
        ICameraController CameraController { get; }
        INPCManagerController NPCManagerController { get; }
        IGlobalSoundController GlobalSoundController { get; }
        IRobotsManagerController RobotsManagerController { get; }
    }
}
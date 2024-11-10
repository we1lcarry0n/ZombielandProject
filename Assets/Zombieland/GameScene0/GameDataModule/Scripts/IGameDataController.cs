using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GameDataModule
{
    public interface IGameDataController
    {
        IRootController RootController { get; }
        void SaveDada<T>(string name,T data);
        T GetData<T>(string name);
    }
}

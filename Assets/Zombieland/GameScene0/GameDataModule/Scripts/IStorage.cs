namespace Zombieland.GameScene0.GameDataModule
{
    public interface IStorage
    {
        void SaveDada<T>(string name,T data);
        T GetData<T>(string name);
    }
}
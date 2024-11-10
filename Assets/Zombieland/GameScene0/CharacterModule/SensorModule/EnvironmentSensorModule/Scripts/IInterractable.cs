namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public interface IInterractable
    {
        IController Controller { get; }

        void ToggleInterractable(bool isInRange);
        bool TryInterract(IEnvironmentSensorController environmentSensorController);
    }
}


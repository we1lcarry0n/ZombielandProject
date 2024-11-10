using Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public interface ISensorController
    {
        ICharacterController CharacterController { get; }
        IEnvironmentSensorController EnvironmentSensorController { get; }
        IImpactableSensorController ImpactableSensorController { get; }

    }
}

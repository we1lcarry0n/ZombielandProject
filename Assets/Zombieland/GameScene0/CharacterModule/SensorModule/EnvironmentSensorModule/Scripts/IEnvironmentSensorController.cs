using System;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public interface IEnvironmentSensorController
    {
        event Action<string> OnInterractionZoneEnter;
        event Action<string> OnInterractionZoneExit;

        void InterractionTriggerEnter(bool hasEntered);
        void ExcludeFromInterractions(IInterractable interractable);

        ISensorController SensorController { get; }
    }
}
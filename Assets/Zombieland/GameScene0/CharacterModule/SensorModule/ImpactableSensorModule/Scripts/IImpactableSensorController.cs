using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule
{
    public interface IImpactableSensorController
    {
        ISensorController SensorController { get; }
        List<Impactable> Impactables { get; }
    }
}
using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCImpactableSensorModule
{
    public interface INPCImpactableSensorController
    {
        INPCController NPCController { get; }
        List<Impactable> Impactables { get; }
    }
}
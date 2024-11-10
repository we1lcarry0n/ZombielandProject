using System;

namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public interface INPCAIController
    {
        event Action OnSlotNumber1;
        event Action OnSlotNumber2;
        event Action OnSlotNumber3;
        event Action OnSlotNumber4;
        event Action<bool> OnFire;

        INPCController NPCController { get; }
        bool IsPatrolling { get; }
    }
}
using System;
using System.Numerics;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public interface INPCMovingController
    {
        event Action<float, bool> OnMoving;

        INPCController NPCController { get; }
        void ActivateMoving(bool isActive);
    }
}
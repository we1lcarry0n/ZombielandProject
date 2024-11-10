using System;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public interface INPCPhysicMoving
    {
        event Action<float, bool> OnMoving;

        void Disable();
        void Init(INPCMovingController nPCMovingController);
        void ActivateMoving(bool isActive);
    }
}
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAimingModule
{
    public interface INPCAimingController
    {
        INPCController NPCController { get; }

        Transform GetTarget();
    }
}
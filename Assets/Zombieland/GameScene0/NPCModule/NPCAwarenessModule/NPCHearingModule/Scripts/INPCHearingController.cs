using System;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public interface INPCHearingController
    {
        event Action<IController, bool> OnHearingDetectCharacter;

        bool IsHearingDetect { get; }
        INPCAwarenessController NPCAwarenessController { get; }
    }
}
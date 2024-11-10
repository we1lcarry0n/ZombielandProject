using System;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public interface INPCAwarenessController
    {
        event Action<IController, bool> OnDetectCharacter;

        INPCController NPCController { get; }
        INPCHearingController NPCHearingController { get; }
        INPCVisualController NPCVisualController { get; }
    }
}
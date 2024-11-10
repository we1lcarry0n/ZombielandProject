using System;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public interface INPCVisualController
    {
        event Action<IController, bool> OnVisualDetectCharacter;

        bool IsVisualDetect { get; }
        INPCAwarenessController NPCAwarenessController { get; }
    }
}
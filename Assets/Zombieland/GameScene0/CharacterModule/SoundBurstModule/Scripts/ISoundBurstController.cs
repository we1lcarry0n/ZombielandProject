using System;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public interface ISoundBurstController
    {
        event Action<IController> OnSound;

        ICharacterController CharacterController { get; }
    }
}

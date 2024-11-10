using System;

namespace Zombieland.GameScene0.CharacterModule.StealthModule
{
    public interface IStealthController
    {
        event Action<bool> OnStealth;

        ICharacterController CharacterController { get; }
        bool IsStealth { get; }
    }
}
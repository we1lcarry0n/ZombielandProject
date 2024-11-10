using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AimingModule
{
    public interface IAimingController
    {
        ICharacterController CharacterController { get; }
        
        Transform GetTarget();
    }
}
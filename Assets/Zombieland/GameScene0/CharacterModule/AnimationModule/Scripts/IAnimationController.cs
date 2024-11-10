using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public interface IAnimationController
    {
        event Action<Vector3> OnAnimationMove;
        event Action<bool> OnAnimationAttack;
        event Action<string> OnAnimationCreateWeapon;
        event Action OnAnimationDestroyWeapon;
        event Action OnStep;

        ICharacterController CharacterController { get; }
    }
}
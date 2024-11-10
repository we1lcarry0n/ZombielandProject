using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public interface INPCAnimationController
    {
        event Action<Vector3> OnAnimatorMoveEvent;
        event Action<bool> OnAnimationAttack;
        event Action<string> OnAnimationCreateWeapon;
        event Action OnAnimationDestroyWeapon;
        event Action OnStep;

        INPCController NPCController { get; }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public interface ITakeImpactController
    {
        event Action<Vector3, Vector3> OnApplyImpact;

        ICharacterController CharacterController { get; }
        void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection);
    }
}
using System.Collections.Generic;
using System;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.NPCModule.NPCTakeDamageModule
{
    public interface INPCTakeDamageController
    {
        event Action<Vector3, Vector3> OnApplyImpact;

        INPCController NPCController { get; }
        void ApplyImpact(List<DirectImpactData> damageTaken, Vector3 impactCollisionPosition, Vector3 impactDirection);
    }
}
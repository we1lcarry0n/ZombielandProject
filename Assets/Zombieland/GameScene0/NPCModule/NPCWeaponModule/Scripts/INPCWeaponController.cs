using System;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCWeaponModule
{
    public interface INPCWeaponController
    {
        event Action<Weapon> OnShotPerformed;
        event Action OnShotFailed;

        INPCController NPCController { get; }
        IWeapon Weapon { get; }
        Transform WeaponPointFire { get; }
    }
}
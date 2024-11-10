using System;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterWeaponModule
{
    public interface ICharacterWeaponController
    {
        event Action<Weapon> OnShotPerformed;
        event Action OnShotFailed;

        ICharacterController CharacterController { get; }
        IWeapon Weapon { get; }
        Transform WeaponPointFire { get; }
    }
}
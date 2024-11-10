using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public interface IVisualBodyController
    {
        event Action OnWeaponInSceneReady;

        GameObject CharacterInScene { get; }
        Transform CharacterCameraFollow { get; }
        Transform CharacterAimTarget { get; }
        GameObject WeaponInScene { get; }
        List<GameObject> SensorTriggersGameobject { get; }
        ICharacterController CharacterController { get; }
    }
}
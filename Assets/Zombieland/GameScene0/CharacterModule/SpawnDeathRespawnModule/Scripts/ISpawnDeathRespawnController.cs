using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public interface ISpawnDeathRespawnController
    {
        event Action<Vector3, Quaternion> OnSpawn;

        ICharacterController CharacterController { get; }

        void SpawnCharacter();
    }
}
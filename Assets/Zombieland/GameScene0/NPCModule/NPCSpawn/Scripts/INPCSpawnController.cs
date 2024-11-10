using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public interface INPCSpawnController
    {
        event Action<Vector3, Quaternion> OnSpawn;
        INPCController NPCController { get; }
    }
}
using System;
using System.Numerics;


namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    [Serializable]
    public class SpawnData
    {
        public Vector3 Level1DefaultPosition { get; set; }
        public Vector3 Level1DefaultRotation { get; set; }
        public Vector3 Level2DefaultPosition { get; set; }
        public Vector3 Level2DefaultRotation { get; set; }
        public float SpawnRadius { get; set; }
        public SpawnType SpawnType { get; set; }
    }
}
using System;
using System.Numerics;


namespace Zombieland.GameScene0.NPCManagerModule
{
    [Serializable]
    public class NPCSpawnData
    {
        public string NPCJsonFileName { get; set; } // Used for Load NpcData JSON
        public Vector3 SpawnPosition { get; set; }
        public Vector3 PatrolPoint { get; set; } 
    }
}
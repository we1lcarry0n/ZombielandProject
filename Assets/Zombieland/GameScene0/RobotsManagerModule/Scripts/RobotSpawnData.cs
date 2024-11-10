using System;
using System.Numerics;



namespace Zombieland.GameScene0.RobotsManagerModule
{
    [Serializable]
    public class RobotSpawnData
    {
        public string RobotJsonFileName { get; set; } // Used for Load NpcData JSON
        public Vector3 SpawnPosition { get; set; }
        public Vector3 PatrolPoint { get; set; }
    }
}
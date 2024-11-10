using System;
using Newtonsoft.Json;
using Zombieland.GameScene0.NPCModule;

namespace Zombieland.GameScene0.BuffDebuffModule
{
    [Serializable]
    public class BuffDebuffData
    {
        public string ID { get; set; } // Serializable
        public string Name { get; set; } // Serializable
        public string IconID { get; set; } // Serializable
        public string PrefabID { get; set; } // Serializable
        public VFXPosition VFXPosition { get; set;} // Serializable
        public float LifeTime { get; set; } // Serializable
        public float Interval { get; set; } // Serializable
        public DirectImpactData DirectImpactData { get; set; }
        [JsonIgnore] public IController ImpactTarget { get; set; }
        [JsonIgnore] public IController Owner { get; set; }
    }
}
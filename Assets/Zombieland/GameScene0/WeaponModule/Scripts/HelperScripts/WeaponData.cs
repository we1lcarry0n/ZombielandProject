using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.WeaponModule
{
    [Serializable]
    public class WeaponData
    {
        [JsonIgnore] public IController Owner;

        public string ID;
        public string Name;
        public string PrefabName;
        public string VFXPrefabName;
        public string SoundName;
        public List<string> AvailableImpactIDs;
        public float ShootCooldown;
        public float ShotAccuracy;
        public int MaxImpactCount;
        public bool HasTarget;
    }
}
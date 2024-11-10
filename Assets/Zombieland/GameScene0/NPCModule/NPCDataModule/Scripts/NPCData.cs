using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCEquipmentModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NPCData
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string PrefabName { get; set; }
        public string NameAnimatorControllerPC { get; set; }
        public string NameAnimatorControllerMobile { get; set; }

        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }

        public float Speed { get; set; }
        public float StopDistance { get; set; }
        public float HearingDistance { get; set; }
        public float VisualAngle { get; set; }
        public float VisualDistance { get; set; }
        public WeaponPoint WeaponPoint { get; set; }

        public List<NPCEquipmentSlotData> NPCEquipmentSlotDatas { get; set; }
        public int DefaultSlotNumber { get; set; }

        [JsonIgnore] public bool IsDead;
        [JsonIgnore] public float Stamina;
        [JsonIgnore] public bool IsStunned;

        [JsonIgnore] public NPCSpawnData NPCSpawnData { get; set; }
    }
}
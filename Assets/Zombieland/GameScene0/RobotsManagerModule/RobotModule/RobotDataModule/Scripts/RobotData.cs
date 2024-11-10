using Newtonsoft.Json;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule
{
    public class RobotData
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public RobotType RobotType { get; set; }
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
        //public WeaponPoint WeaponPoint { get; set; }

        //public List<NPCEquipmentSlotData> NPCEquipmentSlotDatas { get; set; }
        //public int DefaultSlotNumber { get; set; }

        [JsonIgnore] public bool IsDead;
        [JsonIgnore] public float Stamina;
        [JsonIgnore] public bool IsStunned;

        [JsonIgnore] public RobotSpawnData RobotSpawnData { get; set; }
    }
}
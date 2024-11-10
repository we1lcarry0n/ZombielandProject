using System;

namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    [Serializable]
    public class NPCEquipmentSlotData
    {
        public string WeaponJSONName { get; set; }
        public int SlotNumber { get; set; }
        public int DefaultImpactIndexInList { get; set; }
    }
}
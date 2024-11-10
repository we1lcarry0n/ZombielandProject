using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public class CharacterData
    {
        public float MaxMovingSpeed;
        public float DesignMovingSpeed;
        public float MaxRotationSpeed;
        public float DesignRotationSpeed;

        public SpawnData SpawnData;

        public float HP;
        public float HPMax;
        public float HPDefault;
        public float Stamina;

        public bool IsDead;
        public bool IsStunned;
    }
}
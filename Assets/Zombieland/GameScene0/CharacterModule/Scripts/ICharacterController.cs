using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.InventoryModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeImpactModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;
using Zombieland.GameScene0.CharacterModule.AnimationModule;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.AimingModule;
using Zombieland.GameScene0.CharacterModule.StealthModule;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule;
using Zombieland.GameScene0.CharacterModule.CharacterVFX;
using Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts;
using Zombieland.GameScene0.CharacterModule.CharacterWeaponModule;


namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        IRootController RootController { get; }
        ICharacterDataController CharacterDataController { get; }
        ICharacterWeaponController CharacterWeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
        ICharacterMovingController CharacterMovingController { get; }
        ISensorController SensorController { get; }
        ITakeImpactController TakeImpactController { get; }
        IEquipmentController EquipmentController { get; }
        IInventoryController InventoryController { get; }
        IAnimationController AnimationController { get; }
        ISpawnDeathRespawnController SpawnDeathRespawnController { get; }
        IBuffDebuffController BuffDebuffController { get; }
        IAimingController AimingController { get; }
        IStealthController StealthController { get; }
        ICharacterVFXController CharacterVFXController { get; }
        ISoundBurstController SoundBurstController { get; }
    }
}
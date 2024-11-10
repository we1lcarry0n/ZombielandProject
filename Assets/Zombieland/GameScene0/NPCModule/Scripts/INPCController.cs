using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCAimingModule;
using Zombieland.GameScene0.NPCModule.NPCAIModule;
using Zombieland.GameScene0.NPCModule.NPCAnimationModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;
using Zombieland.GameScene0.NPCModule.NPCBuffDebuffModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCEquipmentModule;
using Zombieland.GameScene0.NPCModule.NPCImpactableSensorModule;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSoundModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCTakeDamageModule;
using Zombieland.GameScene0.NPCModule.NPCVFXModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;
using Zombieland.GameScene0.NPCModule.NPCWeaponModule;

namespace Zombieland.GameScene0.NPCModule
{
    public interface INPCController
    {
        INPCManagerController NPCManagerController { get; }
        NPCSpawnData NPCSpawnData { get; }
        INPCDataController NPCDataController { get; }
        INPCVisualBodyController NPCVisualBodyController { get; }
        INPCSpawnController NPCSpawnController { get; }
        INPCImpactableSensorController NPCImpactableSensorController { get; }
        INPCMovingController NPCMovingController { get; }
        INPCAnimationController NPCAnimationController { get; }
        INPCBuffDebuffController NPCBuffDebuffController { get; }
        INPCTakeDamageController NPCTakeDamageController { get; }
        INPCAIController NPCAIController { get; }
        INPCAwarenessController NPCAwarenessController { get; }
        INPCAimingController NPCAimingController { get; }
        INPCEquipmentController NPCEquipmentController { get; }
        INPCWeaponController NPCWeaponController { get; }
        INPCSoundController NPCSoundController { get; }
        INPCVFXController NPCVFXController { get; }
    }
}
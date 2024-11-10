using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.CharacterWeaponModule;
using Zombieland.GameScene0.ImpactModule;
using Zombieland.GameScene0.NPCModule.NPCWeaponModule;

namespace Zombieland.GameScene0.WeaponModule
{
    public class WeaponResourcer
    {
        public  bool IsReserveResurce = false;

        private IController _weaponController;

        public WeaponResourcer(IController weaponController)
        {
            _weaponController = weaponController;
        }

        public void ResourceOperation(bool isOperation,List<ConsumableResource> consumableResources)
        {
            
            if (!IsReserveResurce && !isOperation) 
            {
                return;
            }

            IsReserveResurce = isOperation;

            if (_weaponController is ICharacterWeaponController characterWeaponController)
            {
                characterWeaponController.CharacterController.EquipmentController.CurrentImpactCount += isOperation ? -1 : 1;

                for (int i = 0; i < consumableResources.Count; i++)
                {
                    switch (consumableResources[i].ResourceType)
                    {
                        case ResourceType.Stamina:
                            if (isOperation)
                            {
                                characterWeaponController.CharacterController.CharacterDataController.CharacterData.Stamina -= consumableResources[i].Value;
                            }
                            else
                            {
                                characterWeaponController.CharacterController.CharacterDataController.CharacterData.Stamina += consumableResources[i].Value;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            if (_weaponController is INPCWeaponController nPCWeaponController)
            {
                nPCWeaponController.NPCController.NPCEquipmentController.CurrentImpactCount += isOperation ? -1 : 1;

                for (int i = 0; i < consumableResources.Count; i++)
                {
                    switch (consumableResources[i].ResourceType)
                    {
                        case ResourceType.Stamina:
                            if (isOperation)
                            {
                                nPCWeaponController.NPCController.NPCDataController.NPCData.Stamina -= consumableResources[i].Value;
                            }
                            else
                            {
                                nPCWeaponController.NPCController.NPCDataController.NPCData.Stamina += consumableResources[i].Value;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
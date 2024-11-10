using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Zombieland.GameScene0.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class BuffDebuffInjection : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public List<IBuffDebuffCommand> Buffs { get; set; }
        public List<IBuffDebuffCommand> Debuffs { get; set; }

        public void Execute()
        {
            foreach (var target in Impact.ImpactData.Targets)
            {
                if (target.Controller is IController controller)
                {
                    if (Buffs != null && Buffs.Count > 0)
                    {
                        foreach (var buff in Buffs)
                        {
                            buff.BuffDebuffData.Owner = (IController)Impact.ImpactData.ImpactOwner;
                            buff.BuffDebuffData.ImpactTarget = (IController)controller;
                        } 

                        if (controller is ICharacterController characterController)
                        {
                            characterController.BuffDebuffController.InjectBuffs(Buffs);
                        }
                        else if (controller is INPCController nPCController)
                        {
                            nPCController.NPCBuffDebuffController.InjectBuffs(Buffs);
                        }
                    }

                    if (Debuffs != null && Debuffs.Count > 0)
                    {
                        foreach (var debuff in Debuffs)
                        {
                            debuff.BuffDebuffData.Owner = (IController)Impact.ImpactData.ImpactOwner;
                            debuff.BuffDebuffData.ImpactTarget = (IController)controller;
                        }
                        if (controller is ICharacterController characterController)
                        {
                            characterController.BuffDebuffController.InjectDebuffs(Debuffs);
                        }
                        else if (controller is INPCController nPCController)
                        {
                            nPCController.NPCBuffDebuffController.InjectDebuffs(Debuffs);
                        }
                    }
                }
            }
            
            Impact.Deactivate();
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}

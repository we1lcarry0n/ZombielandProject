using System.Collections.Generic;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.NPCModule.NPCBuffDebuffModule
{
    public interface INPCBuffDebuffController
    {
        Dictionary<string, IBuffDebuffCommand> Buffs { get; set; }
        Dictionary<string, IBuffDebuffCommand> Debuffs { get; set; }
        INPCController NPCController { get; }
        int CountBuffDebuff { get; }


        void InjectBuffs(List<IBuffDebuffCommand> buffs);

        void InjectDebuffs(List<IBuffDebuffCommand> debuffs);

        DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff);
    }
}
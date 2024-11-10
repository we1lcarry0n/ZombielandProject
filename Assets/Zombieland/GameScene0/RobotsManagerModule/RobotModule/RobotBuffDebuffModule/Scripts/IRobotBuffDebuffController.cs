using System.Collections.Generic;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotBuffDebuffModule
{
    public interface IRobotBuffDebuffController
    {
        Dictionary<string, IBuffDebuffCommand> Buffs { get; set; }
        Dictionary<string, IBuffDebuffCommand> Debuffs { get; set; }
        IRobotController RobotController { get; }
        int CountBuffDebuff { get; }


        void InjectBuffs(List<IBuffDebuffCommand> buffs);

        void InjectDebuffs(List<IBuffDebuffCommand> debuffs);

        DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff);
    }
}
using System.Collections.Generic;
using System.Linq;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.NPCModule.NPCBuffDebuffModule
{
    public class NPCBuffDebuffController : Controller, INPCBuffDebuffController
    {
        public Dictionary<string, IBuffDebuffCommand> Buffs { get; set; }
        public Dictionary<string, IBuffDebuffCommand> Debuffs { get; set; }

        public INPCController NPCController { get; private set; }

        public int CountBuffDebuff => Buffs.Count + Debuffs.Count;


        public NPCBuffDebuffController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;

            Buffs = new Dictionary<string, IBuffDebuffCommand>();
            Debuffs = new Dictionary<string, IBuffDebuffCommand>();
        }


        public override void Disable()
        {
            var countBuffs = Buffs.Count - 1;
            var buffs = Buffs.Values.ToArray();

            for (int i = countBuffs; i >= 0; i--)
            {
                buffs[i].Destroy();
            }

            var countDebuffs = Debuffs.Count - 1;
            var debuffs = Debuffs.Values.ToArray();

            for (int i = countDebuffs; i >= 0; i--)
            {
                debuffs[i].Destroy();
            }

            Buffs.Clear();
            Debuffs.Clear();

            base.Disable();
        }

        public void InjectBuffs(List<IBuffDebuffCommand> buffs)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (!Buffs.ContainsKey(buffs[i].BuffDebuffData.Name))
                {
                    Buffs.Add(buffs[i].BuffDebuffData.Name, buffs[i]);
                    buffs[i].Execute();
                }
            }
        }

        public void InjectDebuffs(List<IBuffDebuffCommand> debuffs)
        {
            for (int i = 0; i < debuffs.Count; i++)
            {
                if (!Buffs.ContainsKey(debuffs[i].BuffDebuffData.Name))
                {
                    Buffs.Add(debuffs[i].BuffDebuffData.Name, debuffs[i]);
                    debuffs[i].Execute();
                }
            }
        }

        public DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff)
        {
            DirectImpactData localBuffDebuff = buffDebuff;

            for (int i = 0; i < Buffs.Count; i++)
            {
                var pair = Buffs.ElementAt(i);
                localBuffDebuff = pair.Value.GetProcessedImpactValue(localBuffDebuff);
            }

            for (int i = 0; i < Debuffs.Count; i++)
            {
                var pair = Debuffs.ElementAt(i);
                localBuffDebuff = pair.Value.GetProcessedImpactValue(localBuffDebuff);
            }

            return localBuffDebuff;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
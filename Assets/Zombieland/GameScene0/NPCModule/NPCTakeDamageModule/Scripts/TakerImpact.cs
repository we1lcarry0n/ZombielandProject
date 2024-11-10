using System.Collections.Generic;
using Zombieland.GameScene0.BuffDebuffModule;



namespace Zombieland.GameScene0.NPCModule.NPCTakeDamageModule
{
    public class TakerImpact
    {
        private readonly INPCController _nPCController;

        public TakerImpact(INPCController nPCController)
        {
            _nPCController = nPCController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTakens)
        {
            if (!_nPCController.NPCDataController.NPCData.IsDead)
            {
                for (int i = 0; i < damageTakens.Count; i++)
                {
                    DirectImpactData damageTakenBuffDebuff = _nPCController.NPCBuffDebuffController.GetProcessedImpactValue(damageTakens[i]);

                    if (damageTakenBuffDebuff.AbsoluteValue > 0)
                    {
                        _nPCController.NPCDataController.NPCData.CurrentHealth -= damageTakenBuffDebuff.AbsoluteValue;
                    }

                    if (damageTakenBuffDebuff.PercentageValue > 0)
                    {
                        _nPCController.NPCDataController.NPCData.CurrentHealth -= _nPCController.NPCDataController.NPCData.CurrentHealth / 100 * damageTakenBuffDebuff.PercentageValue;
                    }
                }
            }

            if (_nPCController.NPCDataController.NPCData.CurrentHealth <= 0)
            {
                _nPCController.NPCDataController.NPCData.IsDead = true;
            }
        }
    }
}
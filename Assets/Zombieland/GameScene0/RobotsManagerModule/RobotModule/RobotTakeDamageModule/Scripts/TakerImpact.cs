using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotTakeDamageModule
{
    public class TakerImpact
    {
        private readonly IRobotController _robotController;

        public TakerImpact(IRobotController robotController)
        {
            _robotController = robotController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTakens)
        {
            if (!_robotController.RobotDataController.RobotData.IsDead)
            {
                for (int i = 0; i < damageTakens.Count; i++)
                {
                    DirectImpactData damageTakenBuffDebuff = _robotController.RobotBuffDebuffController.GetProcessedImpactValue(damageTakens[i]);

                    if (damageTakenBuffDebuff.AbsoluteValue > 0)
                    {
                        _robotController.RobotDataController.RobotData.CurrentHealth -= damageTakenBuffDebuff.AbsoluteValue;
                    }

                    if (damageTakenBuffDebuff.PercentageValue > 0)
                    {
                        _robotController.RobotDataController.RobotData.CurrentHealth -= _robotController.RobotDataController.RobotData.CurrentHealth / 100 * damageTakenBuffDebuff.PercentageValue;
                    }
                }
            }

            if (_robotController.RobotDataController.RobotData.CurrentHealth <= 0)
            {
                _robotController.RobotDataController.RobotData.IsDead = true;
            }
        }
    }
}
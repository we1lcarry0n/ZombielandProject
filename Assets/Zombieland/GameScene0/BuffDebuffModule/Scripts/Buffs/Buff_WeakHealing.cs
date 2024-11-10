using System;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.BuffDebuffModule
{
    [Serializable]
    public class Buff_WeakHealing : IBuffDebuffCommand
    {
        public BuffDebuffData BuffDebuffData { get; set; }

        private PeriodicAction _periodicAction;

        public void Execute()
        {
            Debug.Log("WeakTreatment Execute");
            _periodicAction = new PeriodicAction(BuffDebuffData.LifeTime, BuffDebuffData.Interval, IncreaseHP);
            _periodicAction.OnFinished += OnFinishedHandler;
            _periodicAction.Start();
        }

        public void Destroy()
        {
            _periodicAction.Stop();
        }

        public DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff)
        {
            return buffDebuff;
        }

        private void OnFinishedHandler()
        {
            _periodicAction.OnFinished -= OnFinishedHandler;
            SelfDestroy();
        }

        private void SelfDestroy()
        {
            //BuffDebuffData.ImpactTarget.NPCBuffDebuffController.Buffs.Remove(BuffDebuffData.Name);
        }

        private void IncreaseHP(object sender, ElapsedEventArgs e)
        {
            //Debug.Log("IncreaseHP");
            //var HP = BuffDebuffData.ImpactTarget.NPCDataController.NPCData.CurrentHealth + BuffDebuffData.DirectImpactData.AbsoluteValue;

            //if (HP <= BuffDebuffData.ImpactTarget.NPCDataController.NPCData.MaxHealth)
            //{
            //    BuffDebuffData.ImpactTarget.NPCDataController.NPCData.CurrentHealth = HP;
            //}
            //else
            //{
            //    BuffDebuffData.ImpactTarget.NPCDataController.NPCData.CurrentHealth = BuffDebuffData.ImpactTarget.NPCDataController.NPCData.MaxHealth;
            //}
        }
    }
}
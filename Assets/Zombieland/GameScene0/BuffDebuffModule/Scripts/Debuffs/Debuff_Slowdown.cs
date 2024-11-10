using System;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.BuffDebuffModule
{
    [Serializable]
    public class Debuff_Slowdown : IBuffDebuffCommand
    {
        public BuffDebuffData BuffDebuffData { get; set; }

        private PeriodicAction _periodicAction;
        private float _chacheMaxMovingSpeed;

        public void Execute()
        {
            //Debug.Log("Slowdown Execute");
            //_chacheMaxMovingSpeed = BuffDebuffData.ImpactTarget.NPCDataController.NPCData.MaxSpeed;
            //BuffDebuffData.ImpactTarget.NPCDataController.NPCData.MaxSpeed = _chacheMaxMovingSpeed * BuffDebuffData.DirectImpactData.PercentageValue / 100;

            //_periodicAction = new PeriodicAction(BuffDebuffData.LifeTime, BuffDebuffData.Interval, DeSlowdown);
            //_periodicAction.OnFinished += OnFinishedHandler;
            //_periodicAction.Start();
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
            //BuffDebuffData.ImpactTarget.NPCBuffDebuffController.Debuffs.Remove(BuffDebuffData.Name);
        }

        private void DeSlowdown(object sender, ElapsedEventArgs e)
        {
            //Debug.Log("DeSlowdown");
            //BuffDebuffData.ImpactTarget.NPCDataController.NPCData.MaxSpeed = _chacheMaxMovingSpeed;
        }
    }
}
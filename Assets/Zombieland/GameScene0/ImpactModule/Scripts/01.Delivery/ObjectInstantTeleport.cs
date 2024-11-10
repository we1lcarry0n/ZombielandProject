using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectInstantTeleport : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public float Lifetime { get; set; }
        
        private Updater _updater;

        public void Execute()
        {
            var impactObject = Impact.ImpactData.ImpactObject;
            impactObject.transform.position = Impact.ImpactData.ObjectSpawnPosition;
            impactObject.transform.rotation = Impact.ImpactData.ObjectRotation;

            var collisionHandler = Impact.ImpactData.ImpactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery);
            
            if(Lifetime < 0f) return;
            _updater = impactObject.AddComponent<Updater>();
            _updater.SubscribeToUpdate(CheckLifetime);
        }

        private void FinalizeDelivery()
        {
            Deactivate();
            Impact.InitialImpact.Execute();    
        }

        public void Deactivate()
        {
            _updater?.UnsubscribeFromUpdate(CheckLifetime);
        }
        
        private void CheckLifetime()
        {
            Lifetime -= Time.deltaTime;
            if(Lifetime > 0) return;
            FinalizeDelivery();
        }
    }
}

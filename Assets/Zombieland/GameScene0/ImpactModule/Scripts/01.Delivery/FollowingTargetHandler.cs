using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class FollowingTargetHandler : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public float MovingSpeed { get; set; }
        public float Range { get; set; }
        public float Lifetime { get; set; }

        private GameObject _impactObject;
        private Transform _followTargetTransform;
        private Updater _updater;
        private bool _isLifetimeRelated;
        
        private const float PROJECTILE_ROTATION_SPEED = 50f;
        
        public void Execute()
        {
            _impactObject = Impact.ImpactData.ImpactObject;
            _followTargetTransform = Impact.ImpactData.FollowTargetTransform;
            _impactObject.transform.position = Impact.ImpactData.ObjectSpawnPosition;
            _impactObject.transform.rotation = Impact.ImpactData.ObjectRotation;

            var collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery);
            
            _updater = _impactObject.AddComponent<Updater>();
            _updater.SubscribeToUpdate(MoveObject);

            _isLifetimeRelated = Range <= 0;
        }

        private void MoveObject()
        {
            _impactObject.transform.position = Vector3.MoveTowards(_impactObject.transform.position,
                _followTargetTransform.position, MovingSpeed * Time.deltaTime);

            var direction = _followTargetTransform.position - _impactObject.transform.position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _impactObject.transform.rotation = Quaternion.Lerp(_impactObject.transform.rotation, targetRotation,
                PROJECTILE_ROTATION_SPEED * Time.deltaTime);

            if(_isLifetimeRelated)
                CheckLifetime();
            else
                CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_impactObject.transform.position, Impact.ImpactData.ObjectSpawnPosition) < Range) return;
            FinalizeDelivery();
        }

        private void CheckLifetime()
        {
            Lifetime -= Time.deltaTime;
            if(Lifetime > 0) return;
            FinalizeDelivery();
        }

        private void FinalizeDelivery()
        {
            Deactivate();
            Impact.InitialImpact.Execute();
        }

        public void Deactivate()
        {
            _updater?.UnsubscribeFromUpdate(MoveObject);
        }
    }
}

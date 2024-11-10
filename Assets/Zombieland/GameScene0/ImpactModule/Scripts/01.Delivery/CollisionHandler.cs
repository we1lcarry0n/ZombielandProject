using System;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionHandler : MonoBehaviour
    {
        private Action _onObjectCollision;
        public Collider TargetObjectCollider { get; private set; }
        public Vector3 CollisionPosition { get; private set; }
        

        public void Init(Action onObjectCollision)
        {
            _onObjectCollision = onObjectCollision;
        }

        private void OnTriggerEnter(Collider targetObjectCollider)
        {
            TargetObjectCollider = targetObjectCollider;
            CollisionPosition = targetObjectCollider.ClosestPoint(transform.position);
            _onObjectCollision?.Invoke();
        }
    }
}

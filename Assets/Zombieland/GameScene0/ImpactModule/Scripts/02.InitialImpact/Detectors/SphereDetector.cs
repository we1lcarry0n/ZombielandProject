using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class SphereDetector
    {
        public float DetectionRadius { get; set; }

        public List<IImpactable> GetTargets(GameObject impactObject)
        {
            var overlapColliders = Physics.OverlapSphere(impactObject.transform.position, DetectionRadius);
            if (overlapColliders.Length > 0)
            {
                var impactableObjects = new List<IImpactable>();
                foreach (var overlapCollider in overlapColliders)
                {
                    if(overlapCollider.TryGetComponent<IImpactable>(out var impactableObject))
                        impactableObjects.Add(impactableObject);
                }
                return impactableObjects;
            }
            return null;
        }
        
        public List<IImpactable> GetTargets(Vector3 spherePosition)
        {
            var overlapColliders = Physics.OverlapSphere(spherePosition, DetectionRadius);
            if (overlapColliders.Length > 0)
            {
                var impactableObjects = new List<IImpactable>();
                foreach (var overlapCollider in overlapColliders)
                {
                    if(overlapCollider.TryGetComponent<IImpactable>(out var impactableObject))
                        impactableObjects.Add(impactableObject);
                }
                return impactableObjects;
            }
            return null;
        }
    }
}
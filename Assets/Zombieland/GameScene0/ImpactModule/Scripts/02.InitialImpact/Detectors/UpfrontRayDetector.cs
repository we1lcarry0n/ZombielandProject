using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class UpfrontRayDetector
    {
        public float DetectionRadius { get; set; }
        private const float MinCastSphereRadius = 0.2f;

        public void GetTargets(GameObject impactObject, out List<IImpactable> impactablesList,
            out Vector3 collisionPosition)
        {
            var castSphereRadius = impactObject.TryGetComponent<SphereCollider>(out var sphereCollider) 
                ? sphereCollider.radius 
                : MinCastSphereRadius;
            
            var raycastHits = Physics.SphereCastAll(impactObject.transform.position, castSphereRadius, impactObject.transform.forward, DetectionRadius);
            if (raycastHits.Length > 0)
            {
                impactablesList = new List<IImpactable>();
                foreach (var raycastHit in raycastHits)
                {
                    if (raycastHit.collider.TryGetComponent<IImpactable>(out var impactableObject))
                    {
                        impactablesList.Add(impactableObject);
                    }
                }
                collisionPosition = raycastHits[0].point;
            }

            impactablesList = null;
            collisionPosition = Vector3.zero;
        }
    }
}

using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class ObjectParentAssembler : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public string PrefabName { get; set; }

        public void Execute()
        {
            var impactObjectPrefab = Resources.Load<GameObject>(PrefabName);
            Impact.ImpactData.ImpactObject = GameObject.Instantiate(impactObjectPrefab, Impact.ImpactData.ObjectParentTransform);
            
            Impact.Delivery.Execute();
        }

        public void Deactivate()
        {
            GameObject.Destroy(Impact.ImpactData.ImpactObject);
        }
    }
}

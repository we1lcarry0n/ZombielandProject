using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.ImpactModule
{
    public class BaseHealing : IInitialImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }
        public string OnTargetEffectPrefabName { get; set; }
        public float Force { get; set; }

        public void Execute()
        {
            var effectPrefab = Resources.Load<GameObject>(OnTargetEffectPrefabName);
            foreach (var target in Impact.ImpactData.Targets)
            {
                if (target.Controller is ICharacterController characterController)
                {
                    characterController.TakeImpactController.ApplyImpact(InitialImpactData, Vector3.zero, Vector3.zero);
                }

                if (target.Controller is INPCController nPCController)
                {
                    nPCController.NPCTakeDamageController.ApplyImpact(InitialImpactData, Vector3.zero, Vector3.zero);
                }

                if (!effectPrefab) return;
                var effect = GameObject.Instantiate(effectPrefab, ((Impactable)target).transform.position, Quaternion.identity);
                var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                GameObject.Destroy(effect, effectTime);
            }
            Impact.BuffDebuffInjection.Execute();
        }
        
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}

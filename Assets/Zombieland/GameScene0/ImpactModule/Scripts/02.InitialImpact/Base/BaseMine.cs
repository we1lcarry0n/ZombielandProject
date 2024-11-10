using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.ImpactModule
{
    public class BaseMine : IInitialImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public SphereDetector Detector { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }
        public float LifeTime { get; set; }
        public string ExplosionEffectPrefabName { get; set; }
        public string OnTargetEffectPrefabName { get; set; }
        public float Force { get; set; }

        private Updater _updater;

        public void Execute()
        {
            if (LifeTime <= 0)
            {
                ActivateMine();
            }
            else
            {
                _updater = Impact.ImpactData.ImpactObject.AddComponent<Updater>();
                _updater.SubscribeToUpdate(CheckLifetime);
            }
        }

        private void CheckLifetime()
        {
            LifeTime -= Time.deltaTime;
            if(LifeTime > 0) return;
            ActivateMine();
        }

        private void ActivateMine()
        {
            var explosionEffectPrefab = Resources.Load<GameObject>(ExplosionEffectPrefabName);
            var explosionEffect = GameObject.Instantiate(explosionEffectPrefab, Impact.ImpactData.ImpactObject.transform.position, Quaternion.identity);
            var explosionEffectTime = explosionEffect.GetComponent<ParticleSystem>().main.duration;
            GameObject.Destroy(explosionEffect, explosionEffectTime);
            
            var targetsList = Detector.GetTargets(Impact.ImpactData.ImpactObject);
            Impact.ImpactData.Targets = targetsList;

            if (targetsList != null && targetsList.Count > 0)
            {
                var onTargetEffectPrefab = Resources.Load<GameObject>(OnTargetEffectPrefabName);
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

                    if (!onTargetEffectPrefab) return;
                    var effect = GameObject.Instantiate(onTargetEffectPrefab, ((Impactable)target).gameObject.transform);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
            }
            Impact.BuffDebuffInjection.Execute();
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}

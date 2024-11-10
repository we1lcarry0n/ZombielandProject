using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class TestFireHandler : MonoBehaviour
    {
        public string ImpactName;
        public Transform HeroWeaponTransform;
        public Transform FollowTargetTransform;
        public Transform SpawnPosition;
        [SerializeReference] public List<Impactable> TargetImpactableList;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var textAsset = Resources.Load<TextAsset>(ImpactName);
                if (textAsset == null)
                    Debug.LogError("Cannot find file at " + ImpactName);
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
                var impact = JsonConvert.DeserializeObject<Impact>(textAsset.text, settings);
                impact.ImpactData.ObjectSpawnPosition = !SpawnPosition ? HeroWeaponTransform.position : SpawnPosition.position;
                impact.ImpactData.ObjectRotation = HeroWeaponTransform.rotation;
                impact.ImpactData.FollowTargetTransform = FollowTargetTransform;

                if (TargetImpactableList.Count > 0)
                {
                    var targets = new List<IImpactable>(TargetImpactableList);
                    impact.ImpactData.Targets = targets;
                }

                impact.Activate();
            }
        }
    }
}

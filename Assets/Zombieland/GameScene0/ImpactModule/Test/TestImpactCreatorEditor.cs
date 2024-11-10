using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.BuffDebuffModule;


#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Zombieland.GameScene0.ImpactModule.Test
{
    [CustomEditor(typeof(TestImpactCreator))]
    public class TestImpactCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TestImpactCreator testImpactCreator = (TestImpactCreator)target;

            if (GUILayout.Button("Save ImpactData"))
            {
                var fileName = "Test.txt";
                var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/ImpactModule/Resources", fileName);
                Debug.Log($"Save data to filepath: {filePath}");
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
                var json = JsonConvert.SerializeObject(GetImpact(), settings);
                File.WriteAllText(filePath, json);
            }
        } 
        private Impact GetImpact()
        {
            var Impact = new Impact
            {
                ImpactData = new ImpactData()
                {
                    ID = "Wrench",
                    Name = "Wrench",
                    IconID = "IconID",
                    ConsumableResources = new List<ConsumableResource>()
                    {
                        new ConsumableResource()
                        {
                            ResourceType = ResourceType.None,
                            Value = 0f
                        }
                    }
                },

                Assembler = new ObjectParentAssembler()
                {
                    PrefabName = "Wrench"
                },
                
                Delivery = new ObjectInstantTeleport()
                {
                    Lifetime = 1f
                },
                
                InitialImpact = new Wrench()
                {
                    Detector = new TouchColliderDetector()
                    {
                        //DetectionRadius = 0.3f
                    },
                    
                    InitialImpactData = new List<DirectImpactData>()
                    {
                        new DirectImpactData
                        {
                            Type = DirectImpactType.None,
                            AbsoluteValue = 15f,
                            PercentageValue = 0f
                        }
                    },
                    
                    TargetReachedEffectPrefabName = "WrenchTargetReachedEffect",
                    Force = 0f
                },
                
                BuffDebuffInjection = new BuffDebuffInjection
                {
                    // Buffs = new List<IBuffDebuffCommand>()
                    // {
                    //     new Buff_WeakHealing()
                    //     {
                    //         BuffDebuffData = new BuffDebuffData
                    //         {
                    //             ID = "ID",
                    //             Name = "Name",
                    //             IconID = "IconID",
                    //             PrefabID = "PrefabID",
                    //             VFXPosition = VFXPosition.None,
                    //             LifeTime = 0f,
                    //             Interval = 0f,
                    //             DirectImpactData = new DirectImpactData
                    //             {
                    //                 Type = DirectImpactType.None,
                    //                 AbsoluteValue = 0f,
                    //                 PercentageValue = 0f
                    //             }
                    //         }
                    //     }
                    // },
                    // Debuffs = new List<IBuffDebuffCommand>()
                    // {
                    // }
                }
            };
            
            return Impact;
        }
    }
}
#endif
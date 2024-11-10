using SteamAudio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class GlobalSoundController : Controller, IGlobalSoundController
    {
        public IRootController RootController { get; private set; }
        public AudioMixer MainAudioMixer { get; private set; }
        public List<AudioSourceObject> AudioSourceObjects { get; private set; }

        private GlobalAudiosource _globalAudiosource;

        public GlobalSoundController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
            MainAudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
        }

        public override void Disable()
        {
            _globalAudiosource.Destroy();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _globalAudiosource = new GlobalAudiosource();
            _globalAudiosource.CreateGlobalAudioSource();

            RootController.EnvironmentController.OnLoadedSoundGameobjects += SceneLoadedHandler;
        }


        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }



        private void SceneLoadedHandler(List<AudioSourceObject> audioSourceObjects)
        {
            AudioSourceObjects = audioSourceObjects;
            foreach (var audioSourceObject in AudioSourceObjects) 
            {
                SteamAudioSource steamAudioSource = audioSourceObject.AudioSourceObjectInScene.AddComponent<SteamAudioSource>();
                steamAudioSource.directBinaural = true;
                steamAudioSource.distanceAttenuation = true;
                steamAudioSource.distanceAttenuationInput = DistanceAttenuationInput.PhysicsBased;
                steamAudioSource.airAbsorption = true;
                steamAudioSource.airAbsorptionInput = AirAbsorptionInput.SimulationDefined;
                steamAudioSource.occlusion = true;
                steamAudioSource.occlusionInput = OcclusionInput.SimulationDefined;
                steamAudioSource.occlusionType = OcclusionType.Raycast;
                steamAudioSource.transmission = true;
                steamAudioSource.transmissionType = TransmissionType.FrequencyIndependent;
                steamAudioSource.transmissionInput = TransmissionInput.SimulationDefined;
                steamAudioSource.reflections = true;
                steamAudioSource.reflectionsType = ReflectionsType.Realtime;
                steamAudioSource.reflectionsMixLevel = 1.5f;


                AudioDistanceController audioDistanceController = audioSourceObject.AudioSourceObjectInScene.AddComponent<AudioDistanceController>();
                audioDistanceController.Init(this, audioSourceObject);
            }
        }
    }
}
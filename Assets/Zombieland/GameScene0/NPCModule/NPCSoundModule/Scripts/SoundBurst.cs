using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSoundModule
{
    public class SoundBurst
    {
        private INPCSoundController _nPCSoundController;
        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _sounds;
        private LayerMask _wallLayer = LayerMask.GetMask("Wall");


        public SoundBurst(INPCSoundController nPCSoundController)
        {
            _nPCSoundController = nPCSoundController;
            _audioSource = _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<AudioSource>();
            _sounds = new Dictionary<string, AudioClip>();
        }

        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }

            Vector3 npcPosition = _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.transform.position;
            Vector3 playerPosition = _nPCSoundController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform.position;

            float distance = Vector3.Distance(npcPosition, playerPosition);

            if (distance <= _audioSource.maxDistance)
            {
                if (!Physics.Linecast(npcPosition, playerPosition, _wallLayer))
                {
                    AudioClip clip = _sounds[soundName];
                    float mixerEffectsVolume;
                    _nPCSoundController.NPCController.NPCManagerController.RootController.GlobalSoundController.MainAudioMixer.GetFloat("EffectsVolume", out mixerEffectsVolume);
                    float adjustedVolume = AdjustVolume(clip, mixerEffectsVolume);
                    _audioSource.PlayOneShot(clip, adjustedVolume);
                }
            }
        }

        private float AdjustVolume(AudioClip clip, float targetVolume)
        {
            float maxSample = 0f;
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            foreach (float sample in samples)
            {
                if (Mathf.Abs(sample) > maxSample)
                {
                    maxSample = Mathf.Abs(sample);
                }
            }

            float targetVolumeRemap = Remap(targetVolume, -80f, 0f, 0f, 1f);

            return targetVolumeRemap / maxSample;
        }

        private float Remap(float value, float min1, float max1, float min2, float max2)
        {
            return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
        }
    }
}

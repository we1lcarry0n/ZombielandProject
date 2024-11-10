using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurst
    {
        public event Action OnSound;

        private ISoundBurstController _soundBurstController;
        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _sounds;

        public SoundBurst(ISoundBurstController soundBurstController)
        {
            _soundBurstController = soundBurstController;
            _audioSource = soundBurstController.CharacterController.VisualBodyController.CharacterInScene.GetComponent<AudioSource>();
            _sounds = new Dictionary<string, AudioClip>();
        }

        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }

            AudioClip clip = _sounds[soundName];
            float mixerEffectsVolume;
            _soundBurstController.CharacterController.RootController.GlobalSoundController.MainAudioMixer.GetFloat("EffectsVolume", out mixerEffectsVolume);
            float adjustedVolume = AdjustVolume(clip, mixerEffectsVolume);
            _audioSource.pitch = UnityEngine.Random.Range(.87f, 1.07f);
            _audioSource.PlayOneShot(clip, adjustedVolume);
            

            OnSound?.Invoke();
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
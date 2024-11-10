using UnityEngine;


namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class GlobalAudiosource
    {
        private const string GLOBAL_AUDIOSOURCE_PREFAB = "GlobalAudioSource";

        private GameObject _globalAudioSource;

        public AudioSource CreateGlobalAudioSource()
        {
            GameObject prefab = Resources.Load<GameObject>(GLOBAL_AUDIOSOURCE_PREFAB);

            _globalAudioSource = GameObject.Instantiate(prefab);
            
            return _globalAudioSource.GetComponent<AudioSource>();
        }

        public void Destroy()
        {
            GameObject.Destroy(_globalAudioSource);
        }
    }
}
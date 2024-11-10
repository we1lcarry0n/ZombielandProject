using UnityEngine;
using Zombieland.GameScene0.EnvironmentModule;


namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class AudioDistanceController : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.1f;

        private IGlobalSoundController _globalSoundController;
        private AudioSourceObject _audioSourceObject;
        private AudioSource _audioSource;
        private bool _isPlaying;

        public void Init(IGlobalSoundController globalSoundController, AudioSourceObject audioSourceObject)
        {
            _globalSoundController = globalSoundController;
            _audioSourceObject = audioSourceObject;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.spatialize = true;

            if (_audioSourceObject.PlayMode == EnvironmentModule.PlayMode.Continuous)
            {
                _audioSource.loop = true;
            }
            else
            {
                _audioSource.loop = false;
            }
            
            InvokeRepeating(nameof(OnOff), 0f, INVOKE_REPEATING_TIME);
        }


        private void OnOff()
        {
            if (Vector3.Distance(transform.position, _globalSoundController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform.position) < _audioSource.maxDistance)
            {
                _audioSource.enabled = true;
                if (!_isPlaying)
                {
                    InvokeRepeate();
                }
                _isPlaying = true;
            }
            else
            {
                _audioSource.enabled = false;
                CancelInvoke(nameof(InvokeRepeate));
                CancelInvoke(nameof(PlaySound));
                _isPlaying = false;
            }
        }

        private void InvokeRepeate() 
        {
            switch (_audioSourceObject.PlayMode)
            {
                case EnvironmentModule.PlayMode.Continuous:
                        _audioSource.Play();
                    break;

                case EnvironmentModule.PlayMode.FixedInterval:
                    Invoke(nameof(PlaySound), _audioSourceObject.IntervalFixed);
                    break;

                case EnvironmentModule.PlayMode.RandomInterval:
                    float randomInterval = Random.Range(_audioSourceObject.MinIntervalRandom, _audioSourceObject.MaxIntervalRandom);
                    Invoke(nameof(PlaySound), randomInterval);
                    break;

                default:
                    break;
            }
        }

        private void PlaySound()
        {
            if (_audioSource.enabled)
            {
                _audioSource.Play();

                if (_audioSourceObject.PlayMode == EnvironmentModule.PlayMode.FixedInterval)
                {
                    Invoke(nameof(PlaySound), _audioSourceObject.IntervalFixed + _audioSource.clip.length);
                }
                else if (_audioSourceObject.PlayMode == EnvironmentModule.PlayMode.RandomInterval)
                {
                    float randomInterval = Random.Range(_audioSourceObject.MinIntervalRandom, _audioSourceObject.MaxIntervalRandom);
                    Invoke(nameof(PlaySound), randomInterval + _audioSource.clip.length);
                }
            }
        }
    }
}
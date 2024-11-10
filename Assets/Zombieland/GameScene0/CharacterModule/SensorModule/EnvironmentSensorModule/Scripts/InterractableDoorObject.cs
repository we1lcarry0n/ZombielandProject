using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class InterractableDoorObject : MonoBehaviour, IInterractable
    {
        public IController Controller { get; private set; }

        [SerializeField] private Animator[] animators;
        [SerializeField] private bool _isOpened;

        private bool _isInInterractionRange;

        public bool TryInterract(IEnvironmentSensorController environmentSensorController)
        {
            if (!_isInInterractionRange)
            {
                return false;
            }
            Activate();
            return true;
        }

        public void ToggleInterractable(bool isInRange)
        {
            _isInInterractionRange = isInRange;
        }

        public void Activate()
        {
            if (_isOpened)
            {
                foreach (Animator animator in animators)
                {
                    animator.Play("CloseDoor", 0);
                }
            }
            else
            {
                foreach (Animator animator in animators)
                {
                    animator.Play("OpenDoor", 0);
                }
            }
            _isOpened = !_isOpened;
        }
    }
}



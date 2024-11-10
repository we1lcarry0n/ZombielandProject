using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.StealthModule
{
    public class StealthSensor : MonoBehaviour
    {
        public event Action OnDetected;

        private IStealthController _stealthController;

        public void Init(IStealthController stealthController)
        {
            _stealthController = stealthController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC") && _stealthController.IsStealth)
            {
                OnDetected?.Invoke();
            }
        }
    }
}
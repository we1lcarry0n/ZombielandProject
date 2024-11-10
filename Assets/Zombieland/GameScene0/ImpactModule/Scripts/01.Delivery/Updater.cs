using System;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class Updater : MonoBehaviour
    {
        public event Action OnUpdate;

        void Update()
        {
            OnUpdate?.Invoke();
        }

        public void SubscribeToUpdate(Action action)
        {
            OnUpdate += action;
        }
        
        public void UnsubscribeFromUpdate(Action action)
        {
            OnUpdate -= action;
        }
    }
}

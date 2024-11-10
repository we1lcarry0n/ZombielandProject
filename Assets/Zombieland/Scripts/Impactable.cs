using UnityEngine;

namespace Zombieland
{
    public class Impactable : MonoBehaviour, IImpactable
    {
        public IController Controller { get; private set; }

        public void Init(IController controller)
        {
            Controller = controller;
        }
    }
}
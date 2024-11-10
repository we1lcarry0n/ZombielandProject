using System;
using System.Linq;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public class RobotDeadBodySensor : MonoBehaviour
    {
        public event Action<IController> OnDeadBodyDetected;

        private const float DETECTION_RANGE = 10.0f;
        private const float CHECK_INTERVAL = 0.2f;


        public void StartSensor()
        {
            InvokeRepeating(nameof(DetectDeadBody), 0, CHECK_INTERVAL);
        }

        public void StopSensor()
        {
            CancelInvoke(nameof(DetectDeadBody));
        }

        private void DetectDeadBody()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, DETECTION_RANGE);
            hitColliders = hitColliders.Where(hitCollider => hitCollider.GetComponentInChildren<Impactable>() != null).ToArray();

            if (hitColliders.Length == 0)
                return;

            foreach (var hitCollider in hitColliders)
            {
                Impactable[] impactables = hitCollider.GetComponentsInChildren<Impactable>();

                foreach (var impactable in impactables)
                {
                    NPCController controller = impactable.Controller as NPCController;
                    if (controller != null && controller.NPCDataController.NPCData.IsDead && impactable.Controller != null)
                    {
                        OnDeadBodyDetected?.Invoke(impactable.Controller);
                        return;
                    }
                }
            }
        }
    }
}
using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCDetect : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.1f;

        private INPCAIController _nPCAIController;
        private NavMeshAgent _navMeshAgent;
        private Transform _transformDestenation;
        private bool _isInvokeStart;

        public void Init(INPCAIController nPCAIController)
        {
            _nPCAIController = nPCAIController;
            _navMeshAgent = _nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<NavMeshAgent>();
        }

        public void StartDestenation(Transform transformDestenation)
        {
            _transformDestenation = transformDestenation;
            if (!_isInvokeStart)
            {
                InvokeRepeating(nameof(UpdateDestenation), 0f, INVOKE_REPEATING_TIME);
                _isInvokeStart = true;
            }
        }

        public void StopDestenation()
        {
            if (_isInvokeStart)
            {
                CancelInvoke(nameof(UpdateDestenation));
                _isInvokeStart = false;
            }
        }

        private void UpdateDestenation()
        {
            if (_navMeshAgent.enabled)
            {
                _navMeshAgent.SetDestination(_transformDestenation.position);
            }
        }

        private void OnDisable()
        {
            if (_isInvokeStart)
            {
                CancelInvoke(nameof(UpdateDestenation));
                _isInvokeStart = false;
            }
        }
    }
}
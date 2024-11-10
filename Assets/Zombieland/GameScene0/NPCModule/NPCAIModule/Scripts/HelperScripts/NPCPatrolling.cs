using UnityEngine.AI;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCPatrolling : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.5f;

        private INPCAIController _nPCAIController;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _positionSpawn;
        private Vector3 _positionPatrol;
        private bool isGoingToPositionSpawn = false;
        private bool _isInvokeStart;


        public void Init(INPCAIController nPCAIController) 
        {
            _nPCAIController = nPCAIController;
            _navMeshAgent = _nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<NavMeshAgent>();
            _navMeshAgent.stoppingDistance = _nPCAIController.NPCController.NPCDataController.NPCData.StopDistance;

            System.Numerics.Vector3 positionSpawn = _nPCAIController.NPCController.NPCDataController.NPCData.NPCSpawnData.SpawnPosition;
            _positionSpawn = new Vector3(positionSpawn.X, positionSpawn.Y, positionSpawn.Z);

            System.Numerics.Vector3 positionPatrol = _nPCAIController.NPCController.NPCDataController.NPCData.NPCSpawnData.PatrolPoint;
            _positionPatrol = new Vector3(positionPatrol.X, positionPatrol.Y, positionPatrol.Z);
        }

        public void StartPatrolling()
        {
            if (!_isInvokeStart)
            {
                InvokeRepeating(nameof(CheckDestination), 0f, INVOKE_REPEATING_TIME);
                _isInvokeStart = true;
            }
        }

        public void StopPatrolling()
        {
            if (_isInvokeStart)
            {
                CancelInvoke(nameof(CheckDestination));
                _isInvokeStart = false;
            }
        }

        private void CheckDestination() 
        {
            if (_navMeshAgent.enabled)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (isGoingToPositionSpawn)
                    {
                        isGoingToPositionSpawn = false;
                        _navMeshAgent.SetDestination(_positionPatrol);
                    }
                    else
                    {
                        isGoingToPositionSpawn = true;
                        _navMeshAgent.SetDestination(_positionSpawn);
                    }
                }
            }
        }

        private void OnDisable()
        {
            if (_isInvokeStart)
            {
                CancelInvoke(nameof(CheckDestination));
                _isInvokeStart = false;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAIModule
{
    public class RobotPatrolling : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.5f;

        private IRobotAIController _robotAIController;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _positionSpawn;
        private Vector3 _positionPatrol;
        private bool _isGoingToPositionSpawn = false;
        private bool _isInvokeStart;


        public void Init(IRobotAIController robotAIController)
        {
            _robotAIController = robotAIController;
            _navMeshAgent = _robotAIController.RobotController.RobotVisualBodyController.RobotInScene.GetComponent<NavMeshAgent>();
            _navMeshAgent.stoppingDistance = _robotAIController.RobotController.RobotDataController.RobotData.StopDistance;

            System.Numerics.Vector3 positionSpawn = _robotAIController.RobotController.RobotDataController.RobotData.RobotSpawnData.SpawnPosition;
            _positionSpawn = new Vector3(positionSpawn.X, positionSpawn.Y, positionSpawn.Z);

            System.Numerics.Vector3 positionPatrol = _robotAIController.RobotController.RobotDataController.RobotData.RobotSpawnData.PatrolPoint;
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
                    if (_isGoingToPositionSpawn)
                    {
                        _isGoingToPositionSpawn = false;
                        _navMeshAgent.SetDestination(_positionPatrol);
                    }
                    else
                    {
                        _isGoingToPositionSpawn = true;
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
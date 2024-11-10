using System;
using UnityEngine;
using UnityEngine.AI;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotMovingModule;

public class RobotPhysicMovingPC : MonoBehaviour, IRobotPhysicMoving
{
    public event Action<float, bool> OnMoving;

    private const float GRAVITY = 9.8f;

    private float _verticalSpeed;
    private UnityEngine.CharacterController _unityCharacterController;
    private NavMeshAgent _navMeshAgent;
    private IRobotMovingController _robotMovingController;
    private bool _isActive;
    private Vector2 _velocity;
    private Vector2 _smoothDeltaPosition;


    public void Init(IRobotMovingController robotMovingController)
    {
        _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = true;

        _robotMovingController = robotMovingController;

        _isActive = true;
    }

    public void ActivateMoving(bool isActive)
    {
        _unityCharacterController.enabled = isActive;
        _isActive = isActive;
        _navMeshAgent.enabled = isActive;
    }

    public void Disable()
    {
        // Doesn’t have any aplimentation
    }

    public void Move(Vector3 animatorRootPosition)
    {
        if (_unityCharacterController.enabled)
        {
            Vector3 rootPosition = animatorRootPosition;
            rootPosition.y = _navMeshAgent.nextPosition.y;
            transform.position = rootPosition;
            _navMeshAgent.nextPosition = rootPosition;
        }
    }


    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        SynchronizeAnimatorAndNavMeshAgent();
        CalculateGravity();
    }

    private void SynchronizeAnimatorAndNavMeshAgent()
    {
        Vector3 worldDeltaPosition = _navMeshAgent.nextPosition - transform.position;
        worldDeltaPosition.y = 0f;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

        _velocity = _smoothDeltaPosition / Time.deltaTime;
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _velocity = Vector2.Lerp(Vector2.zero, _velocity, _navMeshAgent.remainingDistance / _navMeshAgent.stoppingDistance);
        }

        bool shouldMove = _velocity.magnitude > 0.5f
            && _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance;

        OnMoving?.Invoke(_velocity.magnitude, shouldMove);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > _navMeshAgent.radius / 2f)
        {
            _unityCharacterController.Move(_velocity * Time.deltaTime);
        }
    }

    private void CalculateGravity()
    {
        if (_unityCharacterController.enabled && !_unityCharacterController.isGrounded)
        {
            _verticalSpeed -= _unityCharacterController.isGrounded ? _verticalSpeed : GRAVITY * Time.deltaTime;
            _unityCharacterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
        }
    }
}

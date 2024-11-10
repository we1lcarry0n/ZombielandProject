using System;
using UnityEngine;
using UnityEngine.AI;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAnimationModule
{
    public class RobotAnimator : MonoBehaviour
    {
        //public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;

        private IRobotAnimationController _robotAnimationController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;
        private NavMeshAgent _navMeshAgent;


        public void Init(IRobotAnimationController robotAnimationController)
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(robotAnimationController.RobotController.RobotDataController.RobotData.NameAnimatorControllerPC);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(robotAnimationController.RobotController.RobotDataController.RobotData.NameAnimatorControllerMobile);
#endif

            _robotAnimationController = robotAnimationController;
            _robotAnimationController.RobotController.RobotMovingController.OnMoving += MovingHandler;
            // _nPCAnimatorController.NPCController.NPCAIController.OnFire += AIFireHandler;
        }

        public void Disable()
        {
            _robotAnimationController.RobotController.RobotMovingController.OnMoving -= MovingHandler;
            // _nPCAnimatorController.NPCController.NPCAIController.OnFire -= AIFireHandler;
        }


        private void MovingHandler(float speed, bool isMove)
        {
            if (_animator.GetBool("IsMove") != isMove)
            {
                _animator.SetBool("IsMove", isMove);
            }
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void AIFireHandler(bool isFire)
        {
            if (_robotAnimationController.RobotController.RobotVisualBodyController.WeaponInScene != null)
            {
                _animator.SetBool("IsAttack", isFire);
            }
        }


        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                _robotAnimationController.RobotController.RobotMovingController.RobotPhysicMoving.Move(_animator.rootPosition);
            }
        }
    }
}
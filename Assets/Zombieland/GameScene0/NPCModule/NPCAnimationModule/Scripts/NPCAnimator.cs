using System;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private INPCAnimationController _nPCAnimatorController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;

        public void Init(INPCAnimationController nPCAnimatorController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nPCAnimatorController.NPCController.NPCDataController.NPCData.NameAnimatorControllerPC);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nPCAnimatorController.NPCController.NPCDataController.NPCData.NameAnimatorControllerMobile);
#endif

            _nPCAnimatorController = nPCAnimatorController;
            _nPCAnimatorController.NPCController.NPCMovingController.OnMoving += MovingHandler;
            _nPCAnimatorController.NPCController.NPCEquipmentController.OnWeaponChanged += WeaponChangeHandler;
            _nPCAnimatorController.NPCController.NPCAIController.OnFire += AIFireHandler;


            _animator.SetFloat("Speed", _nPCAnimatorController.NPCController.NPCDataController.NPCData.Speed);
        }


        public void Disable()
        {
            _nPCAnimatorController.NPCController.NPCMovingController.OnMoving -= MovingHandler;
            _nPCAnimatorController.NPCController.NPCEquipmentController.OnWeaponChanged -= WeaponChangeHandler;
            _nPCAnimatorController.NPCController.NPCAIController.OnFire -= AIFireHandler;
        }


        private void MovingHandler(float speed, bool isMove)
        {
            _animator.SetBool("IsMove", isMove);
        }

        private void WeaponChangeHandler(Weapon weapon)
        {
            _weapon = weapon;

            _animator.SetBool("IsHand", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsAK", false);

            if (!_isWeaponAnimation)
            {
                ChangeWeaponAnimation();
            }
        }

        private void ChangeWeaponAnimation()
        {
            switch (_weapon.WeaponData.Name)
            {
                case "Zombie Hand":
                    _animator.SetBool("IsHand", true);
                    _isWeaponAnimation = true;
                    break;

                case "Pistol":
                    _animator.SetBool("IsPistol", true);
                    _isWeaponAnimation = true;
                    break;

                case "AK":
                    _animator.SetBool("IsAK", true);
                    _isWeaponAnimation = true;
                    break;

                default:
                    _isWeaponAnimation = false;
                    _weapon = null;
                    break;
            }
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void AIFireHandler(bool isFire)
        {
            if (_nPCAnimatorController.NPCController.NPCVisualBodyController.WeaponInScene != null)
            {
                _animator.SetBool("IsAttack", isFire);
            }
        }


        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimatorMoveEvent?.Invoke(_animator.rootPosition);
            }
        }

        private void CreacteWeaponPrefabHandler()
        {
            OnAnimationCreateWeapon?.Invoke(_weapon.WeaponData.PrefabName);
        }

        private void DestroyWeaponPrefabHandler()
        {
            OnAnimationDestroyWeapon?.Invoke();
        }

        private void StepHandler()
        {
            OnStep?.Invoke();
        }
    }
}
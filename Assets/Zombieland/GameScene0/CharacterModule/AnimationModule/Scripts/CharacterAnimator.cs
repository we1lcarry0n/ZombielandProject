using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zombieland.GameScene0.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimationMove;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "MobileAnimatorController";
        private const float DAMP_TIME = 0.05f;
        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private IAnimationController _animatorController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;
        private FirePermiser _firePermiser;
        private Rig _multiAimConstraintForBody;

        private GameObject _currentWeaponAimTarget;

        private float _lastFootstep;

        public void Init(IAnimationController animatorController)
        {
            _animator = GetComponent<Animator>();
            _multiAimConstraintForBody = GetComponentInChildren<Rig>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(PC_ANIMATOR);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(MOBILE_ANIMATOR);
#endif

            _animatorController = animatorController;

            _animatorController.CharacterController.EquipmentController.OnWeaponChanged += WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth += StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire += UIFireHandler;

            _firePermiser = new FirePermiser(_animatorController);

            ////Test
            //TestEquipment testEquipment = new TestEquipment(_animatorController);
            //testEquipment.OnWeaponChanged += WeaponChangeHandler;
        }

        public void Disable()
        {
            _animatorController.CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangeHandler;
            _animatorController.CharacterController.StealthController.OnStealth -= StealthHandler;
            _animatorController.CharacterController.RootController.UIController.OnFire -= UIFireHandler;
        }

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _animatorController.CharacterController.CharacterMovingController.RealMovingSpeed);
            _animator.SetFloat("RotationAngle", _animatorController.CharacterController.CharacterMovingController.RotationAngle);
            //Vector2 moveDirection = transform.InverseTransformDirection(_animatorController.CharacterController.CharacterMovingController.DirectionWalk);
            //_animator.SetFloat("DirectionX", Mathf.Round(moveDirection.x));
            //_animator.SetFloat("DirectionY", Mathf.Round(moveDirection.y));

            Vector2 inputVector = _animatorController.CharacterController.CharacterMovingController.DirectionWalk;

            /*if (inputVector.magnitude > 1)
            {
                inputVector = inputVector.normalized;
            }*/

            //inputVector = transform.InverseTransformDirection(inputVector);

            _animator.SetFloat("DirectionX", inputVector.x);
            _animator.SetFloat("DirectionY", inputVector.y);
            StepHandler();
        }


        private void WeaponChangeHandler(Weapon weapon)
        {
            //_multiAimConstraintForBody.weight = 0;
            Debug.Log("Weapon Changed Handler is called");
            _weapon = weapon;

            _animator.SetBool("IsWrench", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsShotgun", false);
            _animator.SetBool("IsAK", false);
            _animator.SetBool("IsWeapon", true);

            if (!_isWeaponAnimation)
            {
                Debug.Log($"is weapon animation = {_isWeaponAnimation} ");
                ChangeWeaponAnimation();
            }
        }

        private void ChangeWeaponAnimation()
        {
            Debug.Log("Received weapon  chenge command");
            switch (_weapon.WeaponData.Name)
            {
                case "Wrench":
                    _animator.SetBool("IsWrench", true);
                    _isWeaponAnimation = true;
                    _multiAimConstraintForBody.weight = 0f;
                    break;

                case "Pistol":
                    _animator.SetBool("IsPistol", true);
                    _isWeaponAnimation = true;
                    _multiAimConstraintForBody.weight = 1f;
                    break;

                case "Shotgun":
                    _animator.SetBool("IsShotgun", true);
                    _isWeaponAnimation = true;
                    break;

                case "AK":
                    _animator.SetBool("IsAK", true);
                    _isWeaponAnimation = true;
                    _multiAimConstraintForBody.weight = 1f;
                    break;

                default:
                    _isWeaponAnimation = false;
                    _weapon = null;
                    //_multiAimConstraintForBody.weight = 0f;
                    break;
            }
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void StealthHandler(bool isStealth)
        {
            _animator.SetBool("IsStealth", isStealth);
        }

        private void UIFireHandler(bool isFire)
        {
            if (_animatorController.CharacterController.VisualBodyController.WeaponInScene != null)
            {
                if (isFire)
                {
                    InvokeRepeating("StartFirePermision", 0, CHECK_FIRE_PERMITION_PERIOD);
                }
                else
                {
                    _animator.SetBool("Attack", isFire);
                    OnAnimationAttack?.Invoke(false);
                }
            }
        }

        private void StartFirePermision()
        {
            if (_firePermiser.CheckFirePermission(_weapon))
            {
                CancelInvoke("StartFirePermision");
                _animator.SetBool("Attack", true);
            }
        }

        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimationMove?.Invoke(_animator.deltaPosition);
            }
        }

        private void CreacteWeaponPrefabHandler()
        {
            OnAnimationCreateWeapon?.Invoke(_weapon.WeaponData.PrefabName);
 /*           GameObject aim = transform.Find($"LeftHandTargetIK{_weapon.WeaponData.Name}").gameObject;
            if (aim != null)
            {
                aim.SetActive(true);
            }*/
        }

        private void DestroyWeaponPrefabHandler()
        {
            _animator.SetBool("IsWrench", false);
            _animator.SetBool("IsPistol", false);
            _animator.SetBool("IsShotgun", false);
            _animator.SetBool("IsAK", false);
            OnAnimationDestroyWeapon?.Invoke();
        }

        private void StepHandler()
        {
            float footstep = _animator.GetFloat("Footstep");
            if (Mathf.Abs(footstep) < .0001f) footstep = 0f;
            if ((footstep > 0 && _lastFootstep <0) || (footstep < 0 && _lastFootstep > 0))
            {
                OnStep?.Invoke();
            }
            _lastFootstep = footstep;
        }
    }
}
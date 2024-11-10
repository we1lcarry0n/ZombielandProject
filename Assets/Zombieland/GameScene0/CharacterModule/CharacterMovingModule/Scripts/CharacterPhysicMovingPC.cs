using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMovingPC : MonoBehaviour, ICharacterPhysicMoving
    {
        private const float GRAVITY = 9.8f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;
        private const float MIN_VECTORMOVE_MAGITUDE = 0.1f;
        private const float DEFAULT_SPEED_MULTIPLIER = 1f;
        private const float FAST_SPEED_MULTIPLIER = 3f;
        private const float DESIRED_ROTATION_THRESHOLD = 20f;

        private Vector2 _vectorMousePosition;
        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private ICharacterMovingController _characterMovingController;
        public bool _isActive;
        private float _speedMultiplier = 1f;
        private float _unityCharacterControllerHeight;
        private Vector3 _unityCharacterControllerCenter;

        private float _currentSpeed;

        private Vector2 actualInputVector;
        private Vector2 currentInputVector;
        private Vector2 smoothInputVelocity;
        private float smoothDampSpeed = .2f;

        private float desiredRotationAngle;
        private float currentRotationAngle;
        private float smoothRotationVelocity;

        private float deltaRotation = 0f;
        private Vector3 initialCameraDirection;
        private int RAYCAST_LAYER_MASKS = Convert.ToInt32("0000001001111", 2);

        #region PUBLIC
        public void Disable()
        {
            _characterMovingController.CharacterController.AnimationController.OnAnimationMove -= OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMoved -= MovedHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMouseMoved -= MovedMouseHandler;
            _characterMovingController.CharacterController.AnimationController.OnAnimationMove -= OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.StealthController.OnStealth -= StealthHandler;
        }

        public void Init(ICharacterMovingController characterMovingController)
        {
            _characterMovingController = characterMovingController;

            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _unityCharacterControllerHeight = _unityCharacterController.height;
            _unityCharacterControllerCenter = _unityCharacterController.center;

            _characterMovingController.CharacterController.AnimationController.OnAnimationMove += OnAnimatorMoveHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMoved += MovedHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnMouseMoved += MovedMouseHandler;
            _characterMovingController.CharacterController.RootController.UIController.OnFastRun += FastRunHandler;
            _characterMovingController.CharacterController.StealthController.OnStealth += StealthHandler;

            _isActive = true;
        }

        public void ActivateMoving(bool isActive)
        {
            _unityCharacterController.enabled = isActive;
            _isActive = isActive;
        }
        #endregion PUBLIC

        #region MONOBEHAVIOUR
        private void Start()
        {
            initialCameraDirection = _characterMovingController.CharacterController.RootController.CameraController.PlayerCamera.transform.forward;
            initialCameraDirection.y = 0;
        }

        private void Update()
        {
            currentInputVector = Vector2.SmoothDamp(currentInputVector, actualInputVector, ref smoothInputVelocity, smoothDampSpeed);
            _characterMovingController.DirectionWalk = currentInputVector;

            /*if (Mathf.Abs(actualRotationDelta) - Mathf.Abs(currentRotationDelta) < .5f)
            {
                currentRotationDelta = 0f;
                actualRotationDelta = 0f;
            }
            else
            {
                currentRotationDelta = Mathf.SmoothDamp(currentRotationDelta, actualRotationDelta, ref smoothRotationVelocity, smoothDampSpeed);
                _characterMovingController.RotationAngle = currentRotationDelta;
            }*/
            currentRotationAngle = Mathf.SmoothDamp(currentRotationAngle, desiredRotationAngle, ref smoothRotationVelocity, smoothDampSpeed);
            _characterMovingController.RotationAngle = currentRotationAngle;
            if (Mathf.Abs(Mathf.Abs(currentRotationAngle) - Mathf.Abs(desiredRotationAngle)) < 1f)
            {
                desiredRotationAngle = 0f;
            }
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;

            CalculateGravity();

            CalculeteRealMovingSpeed();

            if (_vectorMousePosition.magnitude > MIN_VECTORMOVE_MAGITUDE)
            {
                CalculeteRotation();
                CalculateAimTarget();
            }

        }
        #endregion


        #region PRIVATE
        private void OnAnimatorMoveHandler(Vector3 animatorDeltaPosition)
        {
            if (_unityCharacterController.enabled)
            {
                _unityCharacterController.Move(animatorDeltaPosition);
            }
        }

        private void MovedHandler(Vector2 joystickPosition)
        {
            actualInputVector = joystickPosition;
            int x = Mathf.RoundToInt(joystickPosition.x);
            int y = Mathf.RoundToInt(joystickPosition.y);

            Vector2 vectorMove = new Vector2(x, y);

            //if (Mathf.Abs(vectorMove.x) != 0f)
            //{
            //    vectorMove.y = 0f;
            //}

            //_characterMovingController.DirectionWalk = vectorMove;
        }

        private void MovedMouseHandler(Vector2 mousePosition)
        {
            _vectorMousePosition = mousePosition;
        }

        private void CalculateGravity()
        {
            if (_unityCharacterController.enabled && !_unityCharacterController.isGrounded)
            {
                _verticalSpeed -= _unityCharacterController.isGrounded ? _verticalSpeed : GRAVITY * Time.deltaTime;
                _unityCharacterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
            }
        }

        private void FastRunHandler(bool isFastRun)
        {
            _speedMultiplier = isFastRun ? FAST_SPEED_MULTIPLIER : DEFAULT_SPEED_MULTIPLIER;
        }

        private void CalculeteRealMovingSpeed()
        {
            float targetSpeed = Mathf.Clamp01(_characterMovingController.DirectionWalk.magnitude) *
                                _characterMovingController.CharacterController.CharacterDataController.CharacterData.DesignMovingSpeed * _speedMultiplier;

            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * 5f);

            _characterMovingController.RealMovingSpeed = _currentSpeed;
        }


        private void CalculeteRotation()
        {
            /*Vector2 centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 offset = _vectorMousePosition - centerScreen;
            float angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;*/
            float angle = Vector3.Angle(initialCameraDirection, transform.forward);
            Vector3 cameraForward = _characterMovingController.CharacterController.RootController.CameraController.PlayerCamera.transform.forward;
            cameraForward.y = 0;
            transform.rotation = Quaternion.LookRotation(cameraForward);
            //_characterMovingController.CharacterController.VisualBodyController.CharacterCameraFollow.rotation = Quaternion.LookRotation(cameraForward);

            if (Mathf.Abs(Mathf.Abs(deltaRotation) - Mathf.Abs(angle)) < DESIRED_ROTATION_THRESHOLD)
            {
                return;
            }
            if (angle < deltaRotation)
            {
                desiredRotationAngle = Mathf.Abs(Mathf.Abs(deltaRotation) - Mathf.Abs(angle)) * -1f;
                deltaRotation = angle;
            }
            else
            {
                desiredRotationAngle = Mathf.Abs(Mathf.Abs(deltaRotation) - Mathf.Abs(angle));
                deltaRotation = angle;
            }
        }

        private void CalculateAimTarget()
        {
            Vector2 screenCentrePoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = _characterMovingController.CharacterController.RootController.CameraController.PlayerCamera.ScreenPointToRay(screenCentrePoint);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, RAYCAST_LAYER_MASKS))
            {
                _characterMovingController.CharacterController.VisualBodyController.CharacterAimTarget.position = hitInfo.point;
            }
        }

        private void StealthHandler(bool isStealth)
        {
            if (isStealth)
            {
                _unityCharacterController.height = _unityCharacterControllerHeight * 0.75f;
                _unityCharacterController.center = new Vector3(_unityCharacterControllerCenter.x, _unityCharacterControllerCenter.y * 0.75f, _unityCharacterControllerCenter.z);
            }
            else
            {
                _unityCharacterController.height = _unityCharacterControllerHeight;
                _unityCharacterController.center = _unityCharacterControllerCenter;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);

            if (_characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.position,
                    _characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.position +
                    _characterMovingController.CharacterController.CharacterWeaponController.WeaponPointFire.forward * 5f);
            }
        }
#endif

        #endregion PRIVATE
    }
}
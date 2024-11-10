using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public class NPCRagdoll : MonoBehaviour
    {
        private const string STAND_UP_FRONT = "StandUpFront";
        private const string STAND_UP_BACK = "StandUpBack";
        private const float RAGDOLL_TO_MECANIM_BLEND_TIME = 1f;
        private const float DAMAGE_POWER_DIVIDER = 10f;
        private const int RETURN_FROM_RAGDOLL_DELAY_TIME = 100;

        private INPCAnimationController _nPCAnimationController;
        private Animator _animator;
        private Transform _hipsTransform;
        private List<RagdollComponent> _ragdollComponents = new List<RagdollComponent>();
        private RagdollState _ragdollState = RagdollState.Animated;
        private float _ragdollingEndTime;


        #region PUBLIC
        public void Init(INPCAnimationController animationController)
        {
            _nPCAnimationController = animationController;
            _animator = GetComponent<Animator>();
        }

        public async void Hit(Vector3 hitPosition, Vector3 forceDirection)
        {
            foreach (RagdollComponent component in _ragdollComponents)
            {
                Collider collider = component.Collider;

                if (collider != null)
                {
                    if (collider.bounds.Contains(hitPosition))
                    {
                        _nPCAnimationController.NPCController.NPCMovingController.ActivateMoving(false);
                        _animator.enabled = false;

                        ActivateRagdollParts(true);
                        _ragdollState = RagdollState.Ragdolled;

                        component.RigidBody.AddForceAtPosition(forceDirection, hitPosition, ForceMode.Impulse);

                        if (_nPCAnimationController.NPCController.NPCDataController.NPCData.IsDead)
                        {
                            return;
                        }

                        await Task.Delay(RETURN_FROM_RAGDOLL_DELAY_TIME);

                        foreach (RagdollComponent ragdollComponent in _ragdollComponents)
                        {
                            ragdollComponent.IsKinematikBone(true);
                        }

                        _animator.enabled = true;
                        _nPCAnimationController.NPCController.NPCMovingController.ActivateMoving(true);
                    }
                }
            }
        }

        public void GetUp()
        {
            if (_ragdollState != RagdollState.Ragdolled)
            {
                return;
            }

            _ragdollingEndTime = Time.time;
            _animator.enabled = true;
            _ragdollState = RagdollState.BlendToAnimation;

            Vector3 shiftPos = _hipsTransform.position - transform.position;
            shiftPos.y = GetDistanceToFloor(shiftPos.y);

            MoveNodeWithoutChildren(shiftPos);

            foreach (RagdollComponent ragdollComponent in _ragdollComponents)
            {
                ragdollComponent.StoredRotation = ragdollComponent.Transform.localRotation;
                ragdollComponent.PrivRotation = ragdollComponent.Transform.localRotation;

                ragdollComponent.StoredPosition = ragdollComponent.Transform.localPosition;
                ragdollComponent.PrivPosition = ragdollComponent.Transform.localPosition;
            }

            string getUpAnimation = CheckIfLieOnBack() ? STAND_UP_FRONT : STAND_UP_BACK;
            _animator.Play(getUpAnimation, 0, 0);
            ActivateRagdollParts(false);
        }
        #endregion PUBLIC


        #region MONOBEHAVIOUR
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _hipsTransform = _animator.GetBoneTransform(HumanBodyBones.Hips);
            SetAnimationEvent(STAND_UP_FRONT);
            SetAnimationEvent(STAND_UP_BACK);

            for (int i = 0; i < (int)HumanBodyBones.LastBone; i++)
            {
                Transform boneTransform = _animator.GetBoneTransform((HumanBodyBones)i);
                if (boneTransform != null)
                {
                    _ragdollComponents.Add(new RagdollComponent(boneTransform));
                }
            }
        }

        private void LateUpdate()
        {
            if (_ragdollState == RagdollState.BlendToAnimation)
            {
                float ragdollBlendAmount = 1f - Mathf.InverseLerp(_ragdollingEndTime, _ragdollingEndTime + RAGDOLL_TO_MECANIM_BLEND_TIME, Time.time);

                foreach (RagdollComponent ragdollComponent in _ragdollComponents)
                {
                    if (ragdollComponent.PrivRotation != ragdollComponent.Transform.localRotation)
                    {
                        ragdollComponent.PrivRotation = Quaternion.Slerp(ragdollComponent.Transform.localRotation, ragdollComponent.StoredRotation, ragdollBlendAmount);
                        ragdollComponent.Transform.localRotation = ragdollComponent.PrivRotation;
                    }

                    if (ragdollComponent.PrivPosition != ragdollComponent.Transform.localPosition)
                    {
                        ragdollComponent.PrivPosition = Vector3.Slerp(ragdollComponent.Transform.localPosition, ragdollComponent.StoredPosition, ragdollBlendAmount);
                        ragdollComponent.Transform.localPosition = ragdollComponent.PrivPosition;
                    }
                }

                if (Mathf.Abs(ragdollBlendAmount) < Mathf.Epsilon)
                {
                    _ragdollState = RagdollState.Animated;
                }
            }
        }
        #endregion MONOBEHAVIOUR


        #region PRIVATE
        private void SetAnimationEvent(string nameAnimationClip)
        {
            AnimationClip animationClip = GetAnimationClip(nameAnimationClip);
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.functionName = "ActiveUnityCharacterController";
            animationEvent.time = animationClip.length;

            animationClip.AddEvent(animationEvent);
        }

        private AnimationClip GetAnimationClip(string nameAnimationClip)
        {
            RuntimeAnimatorController controller = _animator.runtimeAnimatorController;
            if (controller != null)
            {
                for (int i = 0; i < controller.animationClips.Length; i++)
                {
                    AnimationClip clip = controller.animationClips[i];
                    if (clip.name == nameAnimationClip)
                    {
                        return clip;
                    }
                }
            }

            return null;
        }

        private void ActiveUnityCharacterController()
        {
            _nPCAnimationController.NPCController.NPCMovingController.ActivateMoving(true);
        }

        private float GetDistanceToFloor(float currentY)
        {
            RaycastHit[] hits = Physics.RaycastAll(new Ray(_hipsTransform.position, Vector3.down));
            float distFromFloor = float.MinValue;

            foreach (RaycastHit hit in hits)
                if (!hit.transform.IsChildOf(transform))
                    distFromFloor = Mathf.Max(distFromFloor, hit.point.y);

            if (Mathf.Abs(distFromFloor - float.MinValue) > Mathf.Epsilon)
                currentY = distFromFloor - transform.position.y;

            return currentY;
        }

        private void MoveNodeWithoutChildren(Vector3 shiftPos)
        {
            Vector3 ragdollDirection = GetRagdollDirection();

            _hipsTransform.position -= shiftPos;
            transform.position += shiftPos;

            Vector3 forward = transform.forward;
            transform.rotation = Quaternion.FromToRotation(forward, ragdollDirection) * transform.rotation;
            _hipsTransform.rotation = Quaternion.FromToRotation(ragdollDirection, forward) * _hipsTransform.rotation;
        }

        private Vector3 GetRagdollDirection()
        {
            Vector3 ragdolledFeetPosition = _animator.GetBoneTransform(HumanBodyBones.Hips).position;
            Vector3 ragdolledHeadPosition = _animator.GetBoneTransform(HumanBodyBones.Head).position;
            Vector3 ragdollDirection = ragdolledFeetPosition - ragdolledHeadPosition;
            ragdollDirection.y = 0;
            ragdollDirection = ragdollDirection.normalized;

            if (CheckIfLieOnBack())
            {
                return ragdollDirection;
            }
            else
            {
                return -ragdollDirection;
            }
        }

        private bool CheckIfLieOnBack()
        {
            Vector3 leftUpperLegPosition = _animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position;
            Vector3 rightUpperLegPosition = _animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).position;
            Vector3 hipsPosition = _hipsTransform.position;

            leftUpperLegPosition -= hipsPosition;
            leftUpperLegPosition.y = 0f;
            rightUpperLegPosition -= hipsPosition;
            rightUpperLegPosition.y = 0f;

            Quaternion rotationFromLeftToRight = Quaternion.FromToRotation(leftUpperLegPosition, Vector3.right);
            Vector3 relativePositionToHips = rotationFromLeftToRight * rightUpperLegPosition;

            return relativePositionToHips.z < 0f;
        }

        private async void ActivateRagdollParts(bool activate)
        {
            foreach (RagdollComponent ragdollComponent in _ragdollComponents)
            {
                ragdollComponent.IsKinematikBone(!activate);

                if (!activate)
                {
                    await FixTransformAndEnableJointAsync(ragdollComponent.Joint);
                }
            }
        }

        private async Task FixTransformAndEnableJointAsync(CharacterJoint joint)
        {
            if (joint == null || !joint.autoConfigureConnectedAnchor)
            {
                return;
            }

            SoftJointLimit highTwistLimit = joint.highTwistLimit;
            SoftJointLimit lowTwistLimit = joint.lowTwistLimit;
            SoftJointLimit swing1Limit = joint.swing1Limit;
            SoftJointLimit swing2Limit = joint.swing2Limit;

            SoftJointLimit curHighTwistLimit = highTwistLimit;
            SoftJointLimit curLowTwistLimit = lowTwistLimit;
            SoftJointLimit curSwing1Limit = swing1Limit;
            SoftJointLimit curSwing2Limit = swing2Limit;

            Vector3 startConnectedPosition = joint.connectedBody.transform.InverseTransformVector(joint.transform.position - joint.connectedBody.transform.position);

            float jointParameterChangeDurationTime = 0.3f;
            float timeElapsedStart = 0f;
            float timeElapsedEnd = 1f;
            float maxRotationLimit = 177f;

            joint.autoConfigureConnectedAnchor = false;
            for (float timeElapsed = timeElapsedStart; timeElapsed < timeElapsedEnd; timeElapsed += Time.deltaTime / jointParameterChangeDurationTime)
            {
                Vector3 newConPosition = Vector3.Lerp(startConnectedPosition, joint.connectedAnchor, timeElapsed);
                joint.connectedAnchor = newConPosition;

                curHighTwistLimit.limit = Mathf.Lerp(maxRotationLimit, highTwistLimit.limit, timeElapsed);
                curLowTwistLimit.limit = Mathf.Lerp(-maxRotationLimit, lowTwistLimit.limit, timeElapsed);
                curSwing1Limit.limit = Mathf.Lerp(maxRotationLimit, swing1Limit.limit, timeElapsed);
                curSwing2Limit.limit = Mathf.Lerp(maxRotationLimit, swing2Limit.limit, timeElapsed);

                joint.highTwistLimit = curHighTwistLimit;
                joint.lowTwistLimit = curLowTwistLimit;
                joint.swing1Limit = curSwing1Limit;
                joint.swing2Limit = curSwing2Limit;

                await Task.Yield();
            }

            joint.connectedAnchor = joint.connectedAnchor;
            await Task.Delay(1);
            joint.autoConfigureConnectedAnchor = true;

            joint.highTwistLimit = highTwistLimit;
            joint.lowTwistLimit = lowTwistLimit;
            joint.swing1Limit = swing1Limit;
            joint.swing2Limit = swing2Limit;
        }
        #endregion PRIVATE
    }
}
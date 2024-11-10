using System;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestShooter : MonoBehaviour
    {
        public event Action OnShoot;

        private float _force = 50f;
        private Camera _camera;

        private CharacterRagdoll _characterRagdoll;
        private IAnimationController _animationController;

        public void Init(IAnimationController animationController, CharacterRagdoll characterRagdoll)
        {
            _animationController = animationController;
            _characterRagdoll = characterRagdoll;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (_characterRagdoll != null)
                    {
                        Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
                        forceDirection.y = 0;


                        //Impact impact = _animationController.CharacterController.RootController.GameDataController.GetData<Impact>("GunBullet");

                        //impact.ImpactData.ImpactOwner = (IController)_animationController.CharacterController;

                        //impact.ImpactData.FollowTargetTransform = _animationController.CharacterController.AimingController.GetTarget();

                        //impact.ImpactData.ObjectSpawnPosition = (((_camera.transform.position + hit.point) * 0.5f + hit.point) * 0.5f + hit.point) * 0.5f;

                        //impact.ImpactData.ObjectParentTransform = null;

                        //Vector3 direction = hit.point - _camera.transform.position;
                        //impact.ImpactData.ObjectRotation = Quaternion.LookRotation(direction);

                        //impact.Activate();

                        _animationController.CharacterController.CharacterDataController.CharacterData.IsDead = true;

                        _characterRagdoll.Hit(hit.point, forceDirection * _force);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _characterRagdoll.GetUp();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class VisualBodyController : Controller, IVisualBodyController
    {
        public event Action OnWeaponInSceneReady;

        public GameObject CharacterInScene { get; private set; }
        public Transform CharacterCameraFollow { get; private set; }
        public Transform CharacterAimTarget { get; private set; }
        public GameObject WeaponInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public ICharacterController CharacterController {  get; private set; }

        private CreateCharacterPrefab _createCharacterGameobject;
        private CreateWeaponPrefab _createWeaponPrefab;
        private Transform _characterWeaponPoint;


        public VisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            _createWeaponPrefab = new CreateWeaponPrefab();
            _createCharacterGameobject = new CreateCharacterPrefab();
        }

        public override void Disable()
        {
            _createCharacterGameobject.Destroy(CharacterInScene);
            _createWeaponPrefab.Destroy(WeaponInScene);

            CharacterController.AnimationController.OnAnimationCreateWeapon -= AnimationCreateWeaponHandler;
            CharacterController.AnimationController.OnAnimationDestroyWeapon -= AnimationDestroyWeaponHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            CreateCharacterGameobject();

            GertterTriggers gertterTriggers = new GertterTriggers(this);
            SensorTriggersGameobject = gertterTriggers.GetSensorTriggers();

            CharacterController.AnimationController.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            CharacterController.AnimationController.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }

        private void CreateCharacterGameobject()
        {
            CharacterInScene = _createCharacterGameobject.CreateCharacter(Vector3.zero, Quaternion.identity);
            _characterWeaponPoint = GameObject.Find("CharacterWeaponPoint").transform;
            CharacterCameraFollow = GameObject.Find("CameraTarget").transform;
            CharacterAimTarget = GameObject.Find("AimTarget").transform;
            //CharacterCameraFollow = GameObject.Instantiate(new GameObject("CameraFollow"), cameraFollow).transform;
            CharacterInScene.SetActive(false);
        }

        private void AnimationCreateWeaponHandler(string weaponPrefabName)
        {
            WeaponInScene = _createWeaponPrefab.CtreateWeapon(weaponPrefabName, _characterWeaponPoint);
 
            OnWeaponInSceneReady?.Invoke();
        }

        private void AnimationDestroyWeaponHandler()
        {
            _createWeaponPrefab.Destroy(WeaponInScene);
        }
    }
}

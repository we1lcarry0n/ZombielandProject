using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCVisualBodyController : Controller, INPCVisualBodyController
    {
        public event Action OnWeaponInSceneReady;

        public GameObject NPCInScene { get; private set; }
        public GameObject WeaponInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public INPCController NPCController { get; private set; }


        private CreateNPCPrefab _createNPCGameobject;
        private CreateWeaponPrefab _createWeaponPrefab;
        private Transform _nPCWeaponPoint;
        private NPCHealthBar _nPCHealthBar;

        public NPCVisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
            _createNPCGameobject = new CreateNPCPrefab();
            _createWeaponPrefab = new CreateWeaponPrefab();
        }

        protected override void CreateHelpersScripts()
        {
            CreateNPCGameobject();
            SetTriggersOnNPC();

            NPCController.NPCAnimationController.OnAnimationCreateWeapon += AnimationCreateWeaponHandler;
            NPCController.NPCAnimationController.OnAnimationDestroyWeapon += AnimationDestroyWeaponHandler;

            _nPCHealthBar = NPCInScene.GetComponent<NPCHealthBar>();
            _nPCHealthBar.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void CreateNPCGameobject()
        {
            NPCInScene = _createNPCGameobject.CreateNPC(this, Vector3.zero, Quaternion.identity);
            _nPCWeaponPoint = NPCInScene.GetComponent<NPCWeaponPoint>().GetWeaponPoint(this);
            NPCInScene.SetActive(false);
        }

        private void SetTriggersOnNPC()
        {
            GertterTriggers gertterTriggers = new GertterTriggers(this);
            SensorTriggersGameobject = gertterTriggers.GetSensorTriggers();
        }

        private void AnimationCreateWeaponHandler(string weaponPrefabName)
        {
            WeaponInScene = _createWeaponPrefab.CtreateWeapon(weaponPrefabName, _nPCWeaponPoint);

            OnWeaponInSceneReady?.Invoke();
        }

        private void AnimationDestroyWeaponHandler()
        {
            _createWeaponPrefab.Destroy(WeaponInScene);
        }
    }
}
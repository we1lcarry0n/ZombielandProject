using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public class NPCMovingController : Controller, INPCMovingController
    {
        public event Action<float, bool> OnMoving;

        public INPCController NPCController { get; private set; }

        private INPCPhysicMoving _nPCPhysicMoving;


        public NPCMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }


        public void ActivateMoving(bool isActive)
        {
            _nPCPhysicMoving.ActivateMoving(isActive);
        }

        public override void Disable()
        {
            _nPCPhysicMoving.OnMoving -= MovingHandler;
            _nPCPhysicMoving.Disable();

            base.Disable();
        }


        protected override void CreateHelpersScripts()
        {
#if UNITY_STANDALONE// || UNITY_EDITOR
            _nPCPhysicMoving = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCPhysicMovingPC>();
#else
            _nPCPhysicMoving = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCPhysicMovingMobile>();
#endif

            _nPCPhysicMoving.Init(this);
            _nPCPhysicMoving.OnMoving += MovingHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void MovingHandler(float speed, bool isMove)
        {
            OnMoving?.Invoke(speed, isMove);
        }
    }
}
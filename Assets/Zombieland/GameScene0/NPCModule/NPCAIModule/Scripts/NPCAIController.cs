using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCAIController : Controller, INPCAIController
    {
        public event Action OnSlotNumber1;
        public event Action OnSlotNumber2;
        public event Action OnSlotNumber3;
        public event Action OnSlotNumber4;
        public event Action<bool> OnFire;

        public INPCController NPCController { get; private set; }
        public bool IsPatrolling { get; private set; }

        private NPCPatrolling _nPCPatrolling;
        private NPCDetect _nPCDetect;
        private NPCFire _nPCFire;
        private NPCWeapon _nPCWeapon;

        public NPCAIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
            IsPatrolling = true;
        }

        public override void Disable()
        {
            _nPCPatrolling.StopPatrolling();
            _nPCDetect.StopDestenation();

            _nPCFire.OnFire -= FireHandler;
            NPCController.NPCAwarenessController.OnDetectCharacter -= DetectCharacterHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _nPCPatrolling = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCPatrolling>();
            _nPCPatrolling.Init(this);
            _nPCPatrolling.StartPatrolling();

            _nPCDetect = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCDetect>();
            _nPCDetect.Init(this);

            _nPCFire = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCFire>();
            _nPCFire.Init(this);
            _nPCFire.OnFire += FireHandler;

            _nPCWeapon = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCWeapon>();
            _nPCWeapon.Init(this);
            _nPCWeapon.OnSlotNumber1 += SlotNumber1Handler;
            _nPCWeapon.OnSlotNumber2 += SlotNumber2Handler;
            _nPCWeapon.OnSlotNumber3 += SlotNumber3Handler;
            _nPCWeapon.OnSlotNumber4 += SlotNumber4Handler;

            NPCController.NPCAwarenessController.OnDetectCharacter += DetectCharacterHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void DetectCharacterHandler(IController character, bool isDetect)
        {
            if (isDetect)
            {
                _nPCPatrolling.StopPatrolling();

                ICharacterController characterController = character as ICharacterController;
                if (characterController != null)
                {
                    _nPCDetect.StartDestenation(characterController.VisualBodyController.CharacterInScene.transform);
                }
            }
            else
            { 
                _nPCDetect?.StopDestenation();
                _nPCPatrolling.StartPatrolling();
            }
        }

        private void FireHandler(bool isFire)
        {
            OnFire?.Invoke(isFire);
        }

        private void SlotNumber1Handler()
        {
            OnSlotNumber1?.Invoke();
        }

        private void SlotNumber2Handler()
        {
            OnSlotNumber2?.Invoke();
        }

        private void SlotNumber3Handler()
        {
            OnSlotNumber3?.Invoke();
        }

        private void SlotNumber4Handler()
        {
            OnSlotNumber4?.Invoke();
        }
    }
}
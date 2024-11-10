using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAIModule
{
    public class RobotAIController : Controller, IRobotAIController
    {
        public event Action<bool> OnFire;

        public IRobotController RobotController { get; private set; }
        public bool IsPatrolling { get; private set; }

        private RobotPatrolling _robotPatrolling;
        private RobotDeadBodySnatching _robotDeadBodySnatching;

        public RobotAIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
            IsPatrolling = true;
        }

        public override void Disable()
        {
            _robotPatrolling.StopPatrolling();
            //_nPCDetect.StopDestenation();

            //_nPCFire.OnFire -= FireHandler;
            //NPCController.NPCAwarenessController.OnDetectCharacter -= DetectCharacterHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _robotPatrolling = RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotPatrolling>();
            _robotPatrolling.Init(this);
            _robotPatrolling.StartPatrolling();

            _robotDeadBodySnatching = RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotDeadBodySnatching>();
            _robotDeadBodySnatching.Init(this);

            RobotController.RobotAwarenesController.OnDeadBodyDetected += DeadBodyDetected;


            //_nPCDetect = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCDetect>();
            //_nPCDetect.Init(this);

            //_nPCFire = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCFire>();
            //_nPCFire.Init(this);
            //_nPCFire.OnFire += FireHandler;

            //_nPCWeapon = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCWeapon>();
            //_nPCWeapon.Init(this);
            //_nPCWeapon.OnSlotNumber1 += SlotNumber1Handler;
            //_nPCWeapon.OnSlotNumber2 += SlotNumber2Handler;
            //_nPCWeapon.OnSlotNumber3 += SlotNumber3Handler;
            //_nPCWeapon.OnSlotNumber4 += SlotNumber4Handler;

            //NPCController.NPCAwarenessController.OnDetectCharacter += DetectCharacterHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void DeadBodyDetected(IController controller)
        {
            _robotPatrolling.StopPatrolling();

            INPCController nPCController = controller as INPCController;
            Transform leftFoot = nPCController.NPCVisualBodyController.NPCInScene.GetComponent<NPCWeaponPoint>().LeftFoot;
            _robotDeadBodySnatching.Snatching(leftFoot);
        }
    }
}
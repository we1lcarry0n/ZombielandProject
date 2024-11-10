using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public class NPCHearingController : Controller, INPCHearingController
    {
        public event Action<IController, bool> OnHearingDetectCharacter;

        public bool IsHearingDetect { get; private set; }
        public INPCAwarenessController NPCAwarenessController { get; private set; }

        private HearingSensor _hearingSensor;

        public NPCHearingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCAwarenessController = parentController as INPCAwarenessController;
        }

        public override void Disable()
        {
            _hearingSensor.Destroy();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _hearingSensor = NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<HearingSensor>();
            _hearingSensor.Init(this);
            _hearingSensor.OnHearingDetect += HearingDetectHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void HearingDetectHandler(IController controller, bool isHearing)
        {
            IsHearingDetect = isHearing;
            OnHearingDetectCharacter?.Invoke(controller, isHearing);
        }
    }
}
using System;
using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public class NPCAwarenessController : Controller, INPCAwarenessController
    {
        public event Action<IController, bool> OnDetectCharacter;

        public INPCController NPCController { get; private set; }
        public INPCHearingController NPCHearingController { get; private set; }
        public INPCVisualController NPCVisualController { get; private set; }

        public NPCAwarenessController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        public override void Disable()
        {
            NPCHearingController.OnHearingDetectCharacter -= DetectCharacterHandler;
            NPCVisualController.OnVisualDetectCharacter -= DetectCharacterHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            NPCHearingController = new NPCHearingController(this, new List<IController>
                {
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController,
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.StealthController,
                    (IController)NPCController.NPCVisualBodyController,
                    (IController)NPCController.NPCMovingController
                });
            subsystemsControllers.Add((IController)NPCHearingController);

            NPCVisualController = new NPCVisualController(this, new List<IController>
                {
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.StealthController,
                    (IController)NPCController.NPCVisualBodyController
                });
            subsystemsControllers.Add((IController)NPCVisualController);

            NPCHearingController.OnHearingDetectCharacter += DetectCharacterHandler;
            NPCVisualController.OnVisualDetectCharacter += DetectCharacterHandler;
        }

        private void DetectCharacterHandler(IController controller, bool isDetect)
        {
            if (!isDetect && (NPCHearingController.IsHearingDetect || NPCVisualController.IsVisualDetect))
                return;

            OnDetectCharacter?.Invoke(controller, isDetect);
        }
    }
}
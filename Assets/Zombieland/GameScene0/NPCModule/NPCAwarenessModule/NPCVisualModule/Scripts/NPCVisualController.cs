using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public class NPCVisualController : Controller, INPCVisualController
    {
        public event Action<IController, bool> OnVisualDetectCharacter;

        public bool IsVisualDetect { get; private set; }
        public INPCAwarenessController NPCAwarenessController { get; private set; }

        private VisualSensor _visualSensor;

        public NPCVisualController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCAwarenessController = parentController as INPCAwarenessController;
        }

        protected override void CreateHelpersScripts()
        {
            _visualSensor = NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<VisualSensor>();
            _visualSensor.Init(this);
            _visualSensor.OnVisualDetect += VisualDetectHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void VisualDetectHandler(IController controller, bool isVisible)
        {
            IsVisualDetect = isVisible;
            OnVisualDetectCharacter?.Invoke(controller, isVisible);
        }
    }
}
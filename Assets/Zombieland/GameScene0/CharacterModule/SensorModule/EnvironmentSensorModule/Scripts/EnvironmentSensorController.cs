using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class EnvironmentSensorController : Controller, IEnvironmentSensorController
    {
        public event Action<string> OnInterractionZoneEnter;
        public event Action<string> OnInterractionZoneExit;

        public ISensorController SensorController { get; private set; }

        private InterractableSensor _interractableSensor;

        #region Public
        public EnvironmentSensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            SensorController = parentController as ISensorController;
        }

        public override void Disable()
        {
            SensorController.CharacterController.RootController.UIController.UIMainController.OnUse -= TryInterractHandler;
            base.Disable();
        }

        public void InterractionTriggerEnter(bool hasEntered)
        {
            if (hasEntered)
            {
                OnInterractionZoneEnter?.Invoke("Entered");
            }
            else 
            {
                OnInterractionZoneExit?.Invoke("Exited");
            }
        }

        public void ExcludeFromInterractions(IInterractable interractable)
        {
            _interractableSensor.RemoveInterractable(interractable);
        }
        #endregion

        #region Protected
        protected override void CreateHelpersScripts()
        {
            SensorController.CharacterController.RootController.UIController.UIMainController.OnUse += TryInterractHandler;
            _interractableSensor = SensorController.CharacterController.VisualBodyController.CharacterInScene.AddComponent<InterractableSensor>();
            _interractableSensor.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion

        #region Private
        private void TryInterractHandler()
        {
            _interractableSensor.TryInterract();
            Debug.Log("Trying to interract with something");
        }
        #endregion
    }
}
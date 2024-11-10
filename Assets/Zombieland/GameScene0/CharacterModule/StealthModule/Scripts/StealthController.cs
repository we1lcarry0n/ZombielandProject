using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.StealthModule
{
    public class StealthController : Controller, IStealthController
    {
        public event Action<bool> OnStealth;

        public ICharacterController CharacterController {  get; private set; }
        public bool IsStealth { get; private set; }

        private StealthSensor _stealthSensor;


        public StealthController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        { 
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            if (_stealthSensor != null)
            {
                _stealthSensor.OnDetected -= DetectedHandler;
            }

            CharacterController.RootController.UIController.OnStealth -= StealthHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _stealthSensor = CharacterController.VisualBodyController.CharacterInScene.AddComponent<StealthSensor>();
            _stealthSensor.OnDetected += DetectedHandler;

            CharacterController.RootController.UIController.OnStealth += StealthHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void StealthHandler()
        {
            if (IsStealth)
            {
                IsStealth = false;
            }
            else
            {
                IsStealth = true;
            }

            OnStealth?.Invoke(IsStealth);
            Debug.Log("IsStealth: " + IsStealth);
        }

        private void DetectedHandler()
        {
            if (IsStealth)
            {
                IsStealth = false;
                OnStealth?.Invoke(false);
            }
        }
    }
}
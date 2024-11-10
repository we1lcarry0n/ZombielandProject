using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public class CharacterDataController : Controller, ICharacterDataController
    {
        public CharacterData CharacterData { get; private set; }

        public ICharacterController CharacterController { get; private set; }

        public CharacterDataController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            LoadDefaultValue();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void LoadDefaultValue()
        {
            CharacterData = CharacterController.RootController.GameDataController.GetData<CharacterData>("CharacterData");

            CharacterData.DesignMovingSpeed = CharacterData.MaxMovingSpeed;
            CharacterData.DesignRotationSpeed = CharacterData.MaxRotationSpeed;

            if (CharacterData.HP <= 0)
            {
                CharacterData.HP = CharacterData.HPDefault;
            }
        }
    }
}
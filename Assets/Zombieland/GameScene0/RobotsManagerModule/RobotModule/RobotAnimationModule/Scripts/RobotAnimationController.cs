using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAnimationModule
{
    public class RobotAnimationController : Controller, IRobotAnimationController
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;
        public event Action<string> OnAnimationCreateWeapon;
        public event Action OnAnimationDestroyWeapon;
        public event Action OnStep;

        public IRobotController RobotController { get; private set; }

        private RobotAnimator _robotAnimator;


        public RobotAnimationController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }

        protected override void CreateHelpersScripts()
        {
            _robotAnimator = RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotAnimator>();
            _robotAnimator.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
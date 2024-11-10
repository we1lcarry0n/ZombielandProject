using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.UIModule.UIMainModule;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public event Action<Vector2> OnMoved;
        public event Action<Vector2> OnMouseMoved;
        public event Action<bool> OnFire;
        public event Action<bool> OnFastRun;
        public event Action OnStealth;
        public event Action OnWeaponReaload;
        public event Action OnUse;
        public event Action OnInventory;
        public event Action OnThrow;
        public event Action OnNumber1;
        public event Action OnNumber2;
        public event Action OnNumber3;
        public event Action OnNumber4;

        public IUIMainController UIMainController { get; private set; }


        #region PUBLIC
        public UIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class's constructor doesn't have any content yet.
        }

        public override void Disable()
        {
            if (UIMainController != null)
            {
                UIMainController.OnMoved -= HandleMoved;
                UIMainController.OnMouseMoved -= HandleMouseMoved;
                UIMainController.OnFire -= HandleFireClick;
                UIMainController.OnStealth -= HandleStealthClick;
                UIMainController.OnFastRun -= HandleFastRunClick;
                UIMainController.OnWeaponReaload -= HandleWeaponRealoadClick;
                UIMainController.OnUse -= HandleUseEClick;
                UIMainController.OnInventory -= HandleInventoryEClick;
                UIMainController.OnThrow -= HandleThrowClick;
                UIMainController.OnNumber1 -= HandleNumber1Click;
                UIMainController.OnNumber2 -= HandleNumber2Click;
                UIMainController.OnNumber3 -= HandleNumber3Click;
                UIMainController.OnNumber4 -= HandleNumber4Click;
            }

            base.Disable();
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            // This controller doesn't have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            UIMainController = new UIMainController(this, null);
            subsystemsControllers.Add((IController)UIMainController);

            UIMainController.OnMoved += HandleMoved;
            UIMainController.OnMouseMoved += HandleMouseMoved;
            UIMainController.OnFire += HandleFireClick;
            UIMainController.OnStealth += HandleStealthClick;
            UIMainController.OnFastRun += HandleFastRunClick;
            UIMainController.OnWeaponReaload += HandleWeaponRealoadClick;
            UIMainController.OnUse += HandleUseEClick;
            UIMainController.OnInventory += HandleInventoryEClick;
            UIMainController.OnThrow += HandleThrowClick;
            UIMainController.OnNumber1 += HandleNumber1Click;
            UIMainController.OnNumber2 += HandleNumber2Click;
            UIMainController.OnNumber3 += HandleNumber3Click;
            UIMainController.OnNumber4 += HandleNumber4Click;
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
        }

        private void HandleMouseMoved(Vector2 mousePosition)
        {
            OnMouseMoved?.Invoke(mousePosition);
        }

        private void HandleFireClick(bool isFire)
        {
            OnFire?.Invoke(isFire);
        }

        private void HandleFastRunClick(bool isFastRun)
        {
            OnFastRun?.Invoke(isFastRun);
        }

        private void HandleStealthClick()
        {
            OnStealth?.Invoke();
        }

        private void HandleWeaponRealoadClick()
        {
            OnWeaponReaload?.Invoke();
        }

        private void HandleUseEClick()
        {
            OnUse?.Invoke();
        }

        private void HandleInventoryEClick()
        {
            OnInventory?.Invoke();
        }

        private void HandleThrowClick()
        {
            OnThrow?.Invoke();
        }

        private void HandleNumber1Click()
        {
            OnNumber1?.Invoke();
        }

        private void HandleNumber2Click()
        {
            OnNumber2?.Invoke();
        }

        private void HandleNumber3Click()
        {
            OnNumber3?.Invoke();
        }

        private void HandleNumber4Click()
        {
            OnNumber4?.Invoke();
        }
        #endregion PRIVATE
    }
}
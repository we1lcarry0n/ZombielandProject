using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public class UIMainController : Controller, IUIMainController
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

        private InitializerInputPrefab _initializerInputGameobjects;


        #region PUBLIC
        public UIMainController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class's constructor doesn't have any content yet.
        }

        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _initializerInputGameobjects.Input.OnMoved -= HandleMoved;
                _initializerInputGameobjects.Input.OnMouseMoved -= HandleMouseMoved;
                _initializerInputGameobjects.Input.OnFire -= HandleFireClick;
                _initializerInputGameobjects.Input.OnFastRun -= HandleFastRunClick;
                _initializerInputGameobjects.Input.OnStealth -= HandleStealthClick;
                _initializerInputGameobjects.Input.OnWeaponReaload -= HandleWeaponRealoadClick;
                _initializerInputGameobjects.Input.OnUse -= HandleUseEClick;
                _initializerInputGameobjects.Input.OnInventory -= HandleInventoryEClick;
                _initializerInputGameobjects.Input.OnThrow -= HandleThrowClick;
                _initializerInputGameobjects.Input.OnNumber1 -= HandleNumber1Click;
                _initializerInputGameobjects.Input.OnNumber2 -= HandleNumber2Click;
                _initializerInputGameobjects.Input.OnNumber3 -= HandleNumber3Click;
                _initializerInputGameobjects.Input.OnNumber4 -= HandleNumber4Click;
            }

            base.Disable();
        }
#endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            _initializerInputGameobjects = new InitializerInputPrefab();
            _initializerInputGameobjects.Init();

            _initializerInputGameobjects.Input.OnMoved += HandleMoved;
            _initializerInputGameobjects.Input.OnMouseMoved += HandleMouseMoved;
            _initializerInputGameobjects.Input.OnFire += HandleFireClick;
            _initializerInputGameobjects.Input.OnFastRun += HandleFastRunClick;
            _initializerInputGameobjects.Input.OnStealth += HandleStealthClick;
            _initializerInputGameobjects.Input.OnWeaponReaload += HandleWeaponRealoadClick;
            _initializerInputGameobjects.Input.OnUse += HandleUseEClick;
            _initializerInputGameobjects.Input.OnInventory += HandleInventoryEClick;
            _initializerInputGameobjects.Input.OnThrow += HandleThrowClick;
            _initializerInputGameobjects.Input.OnNumber1 += HandleNumber1Click;
            _initializerInputGameobjects.Input.OnNumber2 += HandleNumber2Click;
            _initializerInputGameobjects.Input.OnNumber3 += HandleNumber3Click;
            _initializerInputGameobjects.Input.OnNumber4 += HandleNumber4Click;

#if UNITY_STANDALONE
            _initializerInputGameobjects.GetInputSystemGameobject().GetComponent<GameCursor>().Init(this);
#endif
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
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

        private void HandleStealthClick()
        {
            OnStealth?.Invoke();
        }

        private void HandleFastRunClick(bool isFastRun)
        {
            OnFastRun?.Invoke(isFastRun);
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
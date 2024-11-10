using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public class Input : MonoBehaviour
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


        private InputSystemControls _inputSystemControls;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();

            _inputSystemControls.Main.Move.performed += context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            _inputSystemControls.Main.Move.canceled += context => OnMoved?.Invoke(new Vector2(0f, 0f));
            _inputSystemControls.Main.MousePosition.performed += context => OnMouseMoved?.Invoke(_inputSystemControls.Main.MousePosition.ReadValue<Vector2>());
            _inputSystemControls.Main.Fire.performed += context => OnFire?.Invoke(true);
            _inputSystemControls.Main.Fire.canceled += context => OnFire?.Invoke(false);
            _inputSystemControls.Main.FastRun.performed += context => OnFastRun?.Invoke(true);
            _inputSystemControls.Main.FastRun.canceled += context => OnFastRun?.Invoke(false);
            _inputSystemControls.Main.Stealth.performed += context => OnStealth?.Invoke();
            _inputSystemControls.Main.WeaponRealod.performed += context => OnWeaponReaload?.Invoke();
            _inputSystemControls.Main.Use.performed += context => OnUse?.Invoke();
            _inputSystemControls.Main.Inventory.performed += context => OnInventory?.Invoke();
            _inputSystemControls.Main.Throw.performed += context => OnThrow?.Invoke();
            _inputSystemControls.Main.Number1.performed += context => OnNumber1?.Invoke();
            _inputSystemControls.Main.Number2.performed += context => OnNumber2?.Invoke();
            _inputSystemControls.Main.Number3.performed += context => OnNumber3?.Invoke();
            _inputSystemControls.Main.Number4.performed += context => OnNumber4?.Invoke();
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            _inputSystemControls.Main.Move.canceled -= context => OnMoved?.Invoke(new Vector2(0f, 0f));
            _inputSystemControls.Main.MousePosition.performed -= context => OnMouseMoved?.Invoke(_inputSystemControls.Main.MousePosition.ReadValue<Vector2>());
            _inputSystemControls.Main.Fire.performed -= context => OnFire?.Invoke(true);
            _inputSystemControls.Main.Fire.canceled -= context => OnFire?.Invoke(false);
            _inputSystemControls.Main.FastRun.performed -= context => OnFastRun?.Invoke(true);
            _inputSystemControls.Main.FastRun.canceled -= context => OnFastRun?.Invoke(false);
            _inputSystemControls.Main.Stealth.performed -= context => OnStealth?.Invoke();
            _inputSystemControls.Main.WeaponRealod.performed -= context => OnWeaponReaload?.Invoke();
            _inputSystemControls.Main.Use.performed -= context => OnUse?.Invoke();
            _inputSystemControls.Main.Inventory.performed -= context => OnInventory?.Invoke();
            _inputSystemControls.Main.Throw.performed -= context => OnThrow?.Invoke();
            _inputSystemControls.Main.Number1.performed -= context => OnNumber1?.Invoke();
            _inputSystemControls.Main.Number2.performed -= context => OnNumber2?.Invoke();
            _inputSystemControls.Main.Number3.performed -= context => OnNumber3?.Invoke();
            _inputSystemControls.Main.Number4.performed -= context => OnNumber4?.Invoke();

            _inputSystemControls.Disable();
        }
    }
}
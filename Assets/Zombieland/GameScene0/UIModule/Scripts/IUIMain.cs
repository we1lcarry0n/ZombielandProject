using System;
using UnityEngine;
using Zombieland.GameScene0.UIModule.UIMainModule;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIMain
    {
        event Action<Vector2> OnMoved;
        event Action<Vector2> OnMouseMoved;
        event Action<bool> OnFire;
        event Action<bool> OnFastRun;
        event Action OnStealth;
        event Action OnWeaponReaload;
        event Action OnUse;
        event Action OnInventory;
        event Action OnThrow;
        event Action OnNumber1;
        event Action OnNumber2;
        event Action OnNumber3;
        event Action OnNumber4;

        IUIMainController UIMainController { get; }
    }
}
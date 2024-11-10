using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public interface IUIMainController
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
    }
}
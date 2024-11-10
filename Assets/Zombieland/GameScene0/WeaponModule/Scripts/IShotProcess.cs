using System;

namespace Zombieland.GameScene0.WeaponModule
{
    public interface IShotProcess
    {
        event Action OnShotPerformed;

        void Init(IController weaponController);
        void StartFire();
        void StopFire();
    }
}
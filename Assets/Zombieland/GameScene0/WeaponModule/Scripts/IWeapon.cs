namespace Zombieland.GameScene0.WeaponModule
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }
        IShotProcess ShotProcess { get; }

        void Init(IController weaponController);
    }
}
namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterPhysicMoving
    {
        void Disable();
        void Init(ICharacterMovingController characterMovingController);
        void ActivateMoving(bool isActive);
    }
}
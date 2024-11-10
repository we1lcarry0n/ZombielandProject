namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public interface ICharacterDataController
    {
        CharacterData CharacterData { get; }
        ICharacterController CharacterController { get; }
    }
}
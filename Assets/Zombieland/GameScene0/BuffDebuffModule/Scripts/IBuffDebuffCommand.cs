namespace Zombieland.GameScene0.BuffDebuffModule
{
    public interface IBuffDebuffCommand : ICommand
    {
        BuffDebuffData BuffDebuffData { get; set; }

        void Destroy();

        DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff);
    }
}
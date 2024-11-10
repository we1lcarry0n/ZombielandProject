namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactCommand : ICommand
    {
        public IImpact Impact { get; set; }
        public void Deactivate();
    }
}

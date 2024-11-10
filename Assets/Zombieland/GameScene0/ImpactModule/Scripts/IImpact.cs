namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpact
    {
        public ImpactData ImpactData { get; set; }
        public IImpactCommand Assembler { get; set; }
        public IImpactCommand Delivery { get; set; }
        public IImpactCommand InitialImpact { get; set; }
        public IImpactCommand BuffDebuffInjection { get; set; }
        public void Activate();
        public void Deactivate();
    }
}
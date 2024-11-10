using System;
using Newtonsoft.Json;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class EmptyDelivery : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }

        public void Execute()
        {
            Impact.InitialImpact.Execute();
        }
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
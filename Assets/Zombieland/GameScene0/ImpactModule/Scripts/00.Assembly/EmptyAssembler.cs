using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class EmptyAssembler : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }

        public void Execute()
        {
            Impact.Delivery.Execute();
        }

        public void Deactivate()
        {
            //Has no implementation
        }
    }
}

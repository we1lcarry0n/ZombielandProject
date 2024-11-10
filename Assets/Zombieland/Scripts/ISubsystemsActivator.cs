using System;
using System.Collections.Generic;

namespace Zombieland
{
    public interface ISubsystemsActivator
    {
        bool IsActive { get; }
        event Action<string> OnReady;
        List<IController> SubsystemsControllers { get; set; }
        void SetSubsystemsActivity(bool isActive);
    }
}
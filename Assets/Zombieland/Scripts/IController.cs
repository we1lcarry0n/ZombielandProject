using System;

namespace Zombieland
{
    public interface IController
    {
        /// <summary>
        /// Is the enable or disable process complete successfully?
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// arg0 - ERROR message or empty string.
        /// arg1 - this controller.
        /// </summary>
        event Action<string, IController> OnReady;


        void Enable();
        void Disable();
    }
}
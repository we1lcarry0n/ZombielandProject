using System;

namespace Zombieland
{
    public interface IDependencyTracker
    {
        /// <summary>
        /// arg0 - ERROR message or empty string.
        /// </summary>
        event Action<string> OnReady;
        
        void Init();
        void Deinit();
    }
}
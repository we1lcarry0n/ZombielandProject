using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland
{
    public abstract class Controller : IController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        protected  IController _parentController;
        private  ISubsystemsActivator _subsystemsActivator;
        private  IDependencyTracker _dependencyTracker;
        private List<IController> _requiredControllers;

/// <summary>
/// 
/// </summary>
/// <param name="parentController"></param>
/// <param name="requiredControllers">list of links to controllers of other modules required to run this system.</param>
        public Controller(IController parentController, List<IController> requiredControllers)
        {
            _parentController = parentController;
            _requiredControllers = requiredControllers;
        }

        public virtual void Enable()
        {
            _dependencyTracker = new DependencyTracker(this, _requiredControllers);
            _subsystemsActivator = new SubsystemsActivator(this);
            _dependencyTracker.OnReady += OnDependencysReadyHandler;
            _dependencyTracker.Init();
        }

        public virtual void Disable()
        {
            _dependencyTracker.OnReady -= OnDependencysReadyHandler;
            _subsystemsActivator.OnReady -= OnSystemReadyHandler;

            _subsystemsActivator.SetSubsystemsActivity(false);
        }


        /// <summary>
        /// If necessary, add the creation of helper scripts to this method.
        /// </summary>
        protected abstract void CreateHelpersScripts();

        /// <summary>
        /// Create subsystem controllers in this method and add them to the list.
        /// </summary>
        /// <param name="subsystemsControllers">reference list into which newly created subsystem controllers must be entered.</param>
        protected abstract void CreateSubsystems(ref List<IController> subsystemsControllers);


        private void OnDependencysReadyHandler(string errorMessage)
        {
            _dependencyTracker.OnReady -= OnDependencysReadyHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                CreateHelpersScripts();
                List<IController> controllers = _subsystemsActivator.SubsystemsControllers;
                CreateSubsystems(ref controllers);
                _subsystemsActivator.OnReady += OnSystemReadyHandler;
                _subsystemsActivator.SetSubsystemsActivity(true);
            }
            else
            {
                OnReady?.Invoke(errorMessage, this);
            }
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            _subsystemsActivator.OnReady -= OnSystemReadyHandler;
            IsActive = string.IsNullOrEmpty(errorMessage);
            if (IsActive)
            {
                Debug.Log($"<color=green>{this.GetType().Name} subsystems are ready!</color>");
            }
            else
            {
                Debug.Log($"<color=red> {this.GetType().Name} System are not ready! </color>");
                Debug.LogError(errorMessage);
            }
            OnReady?.Invoke(errorMessage, this);
        }
    }
}
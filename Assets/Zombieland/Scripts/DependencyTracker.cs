using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Zombieland
{
    public class DependencyTracker : IDependencyTracker
    {
        public event Action<string> OnReady;

        private readonly IController _parentController;
        private List<IController> _requiredControllers;
        private List<IController> _notActiveRequiredControllers;
        private int _counter;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const int ControlTime = 100; //in millisecond


        public DependencyTracker(IController parentController, List<IController> requiredControllers)
        {
            _parentController = parentController;
            _requiredControllers = requiredControllers ?? new List<IController>();
        }

        public void Init()
        {
            Debug.Log($"{_parentController.GetType().Name} Init!");
            CheckRequiredControllersReadiness();
        }

        public void Deinit()
        {
            _cancellationTokenSource.Cancel();
            for (int i = 0; i < _notActiveRequiredControllers.Count; i++)
            {
                _notActiveRequiredControllers[i].OnReady -= OnRequiredControllerReadyHandler;
            }
        }
        

        private void CheckRequiredControllersReadiness()
        {
            _notActiveRequiredControllers = new List<IController>();
            Debug.Log($"{_parentController.GetType().Name}:   Number of dependencies = {_requiredControllers.Count}:");

            for (int i = 0; i < _requiredControllers.Count; i++)
            {
                if (_requiredControllers[i] != null && !_requiredControllers[i].IsActive)
                {
                    Debug.Log($"{_parentController.GetType().Name}:   Required controller -{_requiredControllers[i].GetType().Name}- {(_requiredControllers[i].IsActive ? "Is Active!" : "Is not Active!")}");
                    _notActiveRequiredControllers.Add(_requiredControllers[i]);
                }
            }

            if (_notActiveRequiredControllers.Count == 0)
            {
                OnDependencysReadyHandler(string.Empty);
            }
            else
            {
                for (int i = 0; i < _notActiveRequiredControllers.Count; i++)
                {
                    Debug.Log($"{_parentController.GetType().Name}:   We are waiting activation of -{_notActiveRequiredControllers[i].GetType().Name}-");
                    _notActiveRequiredControllers[i].OnReady += OnRequiredControllerReadyHandler;
                }
            }
        }

        private void OnRequiredControllerReadyHandler(string errorMessage, IController reportingController)
        {
            reportingController.OnReady -= OnRequiredControllerReadyHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                var id = _notActiveRequiredControllers.IndexOf(reportingController);
                _notActiveRequiredControllers.RemoveAt(id);
                if (_notActiveRequiredControllers.Count == 0)
                {
                    OnDependencysReadyHandler(string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < _notActiveRequiredControllers.Count; i++)
                {
                    _notActiveRequiredControllers[i].OnReady -= OnRequiredControllerReadyHandler;
                }

                OnDependencysReadyHandler(
                    $"{this.GetType().Name}: Report from {reportingController.GetType().Name}. Required controller {reportingController.GetType().Name} is crashed!");
            }
        }

        private void OnDependencysReadyHandler(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"<color=green>{_parentController.GetType().Name} All dependencies are ready!</color>");
            }
            else
            {
                Debug.Log(
                    $"<color=red>{_parentController.GetType().Name} Not all dependencies are ready! {errorMessage}</color>");
            }

            OnReady?.Invoke(errorMessage);
        }
    }
}
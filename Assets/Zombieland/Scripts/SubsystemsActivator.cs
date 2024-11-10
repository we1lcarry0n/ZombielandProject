using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland
{
    public sealed class SubsystemsActivator : ISubsystemsActivator
    {
        public bool IsActive => _currentActivity;
        public event Action<string> OnReady;
        public List<IController> SubsystemsControllers { get; set; }

        private readonly IController _parentController;
        private string[] _controllerNames;
        private List<string> _preparedControllerNames = new();
        private StringBuilder _stringBuilder = new();
        private int _counter;
        private bool _targetActivity;
        private bool _currentActivity;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const int ControlTime = 10000; //in millisecond


        public SubsystemsActivator(IController parentController)
        {
            _parentController = parentController;
            SubsystemsControllers = new();
            _controllerNames = SubsystemsControllers.Select(controller => controller.GetType().Name).ToArray();
        }

        public void SetSubsystemsActivity(bool isActive)
        {
            _targetActivity = isActive;

            if (SubsystemsControllers.Count == 0)
            {
                _cancellationTokenSource.Cancel();
                ShowControllersState();
                OnReady?.Invoke(String.Empty);
            }
            else
            {
                StartCountdownCfControlTime();

                _preparedControllerNames.Clear();
                _counter = SubsystemsControllers.Count;
            
                for (int i = 0; i < SubsystemsControllers.Count; i++)
                {
                    SubsystemsControllers[i].OnReady += OnSubsystemReadinessHandler;
                }

                if (isActive)
                {
                    for (int i = 0; i < SubsystemsControllers.Count; i++)
                    {
                        SubsystemsControllers[i].Enable();
                    }
                }
                else
                {
                    for (int i = 0; i < SubsystemsControllers.Count; i++)
                    {
                        SubsystemsControllers[i].Disable();
                    }
                }
            }
        }

        private void StartCountdownCfControlTime()
        {
            Task.Delay(ControlTime, _cancellationTokenSource.Token).ContinueWith(task => {
                if (!task.IsCanceled)
                {
                    OnPassedTimeHandler();
                }
            });
        }

        private void OnSubsystemReadinessHandler(string errorMessage, IController reportingController)
        {
            reportingController.OnReady -= OnSubsystemReadinessHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                _preparedControllerNames.Add(reportingController.GetType().Name);
                _counter--;
                if (_counter == 0)
                {
                    OnAllSubsystemsReadinessHandler(string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < SubsystemsControllers.Count; i++)
                {
                    SubsystemsControllers[i].OnReady -= OnSubsystemReadinessHandler;
                }
                OnAllSubsystemsReadinessHandler($"{this.GetType().Name}: Report from {reportingController.GetType().Name}. System preparation failure! Error message: {errorMessage}");
            }
        }

        private void OnAllSubsystemsReadinessHandler(string errorMessage)
        {
            if (String.IsNullOrEmpty(errorMessage))
            {
                _currentActivity = _targetActivity;
            }
            ShowControllersState();
            _cancellationTokenSource.Cancel();
            OnReady?.Invoke(errorMessage);
        }

        // Disabling startup after time has elapsed.
        private void OnPassedTimeHandler()
        {
            if (_targetActivity != _currentActivity)
            {
                Debug.Log($"The system of <color=yellow>{_parentController.GetType().FullName}</color> was not activated within the allotted time!");
                ShowControllersState();
            }
        }

        private void ShowControllersState()
        {
            var unpreparedControllerNames = _controllerNames.Except(_preparedControllerNames).ToList();
            if (_controllerNames.Length > 0)
            {
                Debug.Log($"Report from {_parentController.GetType().FullName} : All subcontrollers: <color=green>{GetCombinedNamesOf(_controllerNames.ToList())}</color>");
            }
            
            if (_preparedControllerNames.Count > 0)
            {
                Debug.Log($"Report from {_parentController.GetType().FullName} : Prepared controllers: <color=green>{GetCombinedNamesOf(_preparedControllerNames)}</color>");
            }

            if (unpreparedControllerNames.Count > 0)
            {
                Debug.Log($"Report from {_parentController.GetType().FullName} : Unprepared controllers: <color=red>{GetCombinedNamesOf(unpreparedControllerNames)}</color>");
            }
        }

        private StringBuilder GetCombinedNamesOf(List<string> controllerNames)
        {
            _stringBuilder.Clear();
            for (int i = 0; i < controllerNames.Count; i++)
            {
                _stringBuilder.Append(controllerNames[i]);
                _stringBuilder.Append(", ");
            }
            return _stringBuilder;
        }
    }
}
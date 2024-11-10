using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.CharacterModule.AimingModule
{
    public class Aiming
    {
        private Vector2 _mousePos  = new Vector2();
        private Vector2 _crosshairOffset = new Vector2(16f, -16f);
        private Camera _camera;

        private IAimingController _aimingController;

        #region PUBLIC
        public Aiming(IAimingController aimingController) 
        {
            _aimingController = aimingController;
            _aimingController.CharacterController.RootController.UIController.OnMouseMoved += MouseMoveHandler;
        }
        
        public void Disable()
        {
            _aimingController.CharacterController.RootController.UIController.OnMouseMoved -= MouseMoveHandler;
        }

        public Transform GetTarget()
        {
            try
            {
                Ray ray = _aimingController.CharacterController.RootController.CameraController.PlayerCamera.ScreenPointToRay(_mousePos);
                RaycastHit hit;
            
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.TryGetComponent(out Impactable _))
                    {
                        return hit.transform;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("No PlayerCamera");
            }
            return default(Transform);
        }
        #endregion

        #region PRIVATE
        private void MouseMoveHandler(Vector2 mousePos)
        {
            _mousePos = mousePos + _crosshairOffset;
        }
        #endregion
    }
}
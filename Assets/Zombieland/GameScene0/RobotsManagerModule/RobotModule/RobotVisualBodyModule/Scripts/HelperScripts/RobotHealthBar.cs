using UnityEngine;
using UnityEngine.UI;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule
{
    public class RobotHealthBar : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] Slider _slider;

        private IRobotVisualBodyController _robotVisualBodyController;


        public void Init(IRobotVisualBodyController robotVisualBodyController)
        {
            _robotVisualBodyController = robotVisualBodyController;
            _canvas.worldCamera = _robotVisualBodyController.RobotController.RobotsManagerController.RootController.CameraController.PlayerCamera;
            _slider.value = CalculateHealthProcent();

            //_nPCVisualBodyController.NPCController.NPCTakeDamageController.OnApplyImpact += UpdateHealth;
            //_nPCVisualBodyController.NPCController.NPCMovingController.OnMoving += UpdateRotationCanvas;
        }

        private void UpdateHealth(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _slider.value = CalculateHealthProcent();

            if (_robotVisualBodyController.RobotController.RobotDataController.RobotData.CurrentHealth < 0)
            {
                _canvas.gameObject.SetActive(false);
            }
        }

        private float CalculateHealthProcent()
        {
            return _robotVisualBodyController.RobotController.RobotDataController.RobotData.CurrentHealth / _robotVisualBodyController.RobotController.RobotDataController.RobotData.MaxHealth;
        }

        private void UpdateRotationCanvas(float speed, bool isMove)
        {
            _canvas.transform.LookAt(_canvas.transform.position + _robotVisualBodyController.RobotController.RobotsManagerController.RootController.CameraController.PlayerCamera.transform.forward);
        }
    }
}
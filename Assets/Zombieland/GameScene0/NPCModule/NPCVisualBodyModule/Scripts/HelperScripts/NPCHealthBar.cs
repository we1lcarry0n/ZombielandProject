using UnityEngine;
using UnityEngine.UI;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCHealthBar : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] Slider _slider;

        private INPCVisualBodyController _nPCVisualBodyController;


        public void Init(INPCVisualBodyController nPCVisualBodyController)
        {
            _nPCVisualBodyController = nPCVisualBodyController;
            _canvas.worldCamera = _nPCVisualBodyController.NPCController.NPCManagerController.RootController.CameraController.PlayerCamera;
            _slider.value = CalculateHealthProcent();

            _nPCVisualBodyController.NPCController.NPCTakeDamageController.OnApplyImpact += UpdateHealth;
            _nPCVisualBodyController.NPCController.NPCMovingController.OnMoving += UpdateRotationCanvas;
        }

        private void UpdateHealth(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            _slider.value = CalculateHealthProcent();

            if (_nPCVisualBodyController.NPCController.NPCDataController.NPCData.CurrentHealth < 0 || _nPCVisualBodyController.NPCController.NPCDataController.NPCData.IsDead)
            {
                _canvas.gameObject.SetActive(false);
            }
        }

        private float CalculateHealthProcent()
        {
            return _nPCVisualBodyController.NPCController.NPCDataController.NPCData.CurrentHealth / _nPCVisualBodyController.NPCController.NPCDataController.NPCData.MaxHealth;
        }

        private void UpdateRotationCanvas(float speed, bool isMove)
        {
            _canvas.transform.LookAt(_canvas.transform.position + _nPCVisualBodyController.NPCController.NPCManagerController.RootController.CameraController.PlayerCamera.transform.forward);
        }
    }
}
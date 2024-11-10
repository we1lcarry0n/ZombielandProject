using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public class GameCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private IUIMainController _uIMainController;
        private GameObject _aim;
        private Transform _aimTransform;


        public void OnPointerEnter(PointerEventData eventData)
        {
            OnScreenButton onScreenButton = eventData.pointerEnter.GetComponent<OnScreenButton>();
            if (onScreenButton != null)
            {
                Cursor.visible = true;
                _aim.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnScreenButton onScreenButton = eventData.pointerEnter.GetComponent<OnScreenButton>();
            if (onScreenButton != null)
            {
                Cursor.visible = false;
                _aim.SetActive(true);
            }
        }

        public void Init(IUIMainController uIMainController)
        {
            _uIMainController = uIMainController;
            _uIMainController.OnMouseMoved += UpdateAim;
        }

        public void OnDestroy()
        {
            _uIMainController.OnMouseMoved -= UpdateAim;
        }

        private void Start()
        {
            Cursor.visible = false;
            _aim = GameObject.Find("Aim");
            _aimTransform = _aim.GetComponent<Transform>();
        }

        private void UpdateAim(Vector2 mousePosition)
        {
            _aimTransform.position = mousePosition;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.visible = !hasFocus;
            _aim.SetActive(hasFocus);
        }

        private void OnApplicationPause(bool isPaused)
        {
            Cursor.visible = !isPaused;
            _aim.SetActive(isPaused);
        }
    }
}
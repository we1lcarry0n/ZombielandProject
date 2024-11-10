using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public class MovingJoystickScreen : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _rectTransformJoystik;
        [SerializeField] private OnScreenStick _onScreenStickJoystick;

        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = _rectTransformJoystik.localPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rectTransformJoystik.position = eventData.position;
            _onScreenStickJoystick.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _onScreenStickJoystick.OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransformJoystik.localPosition = _startPosition;
            _onScreenStickJoystick.OnPointerUp(eventData);
        }
    }
}
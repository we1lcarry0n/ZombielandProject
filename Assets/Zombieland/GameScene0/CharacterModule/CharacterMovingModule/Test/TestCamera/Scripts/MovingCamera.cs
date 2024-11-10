using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class MovingCamera : MonoBehaviour
    {
        [SerializeField] private string characterObjectName = "Character0(Clone)";
        private GameObject _character;

        private float _smoothSpeedCamera = 0.1f;
        private Vector3 _offsetFromCharacter;

        private void Awake()
        {
            StartCoroutine(WaitForCharacter());
        }

        private System.Collections.IEnumerator WaitForCharacter()
        {
            while (_character == null)
            {
                _character = GameObject.Find(characterObjectName);
                yield return null;
            }
            _offsetFromCharacter = transform.position - _character.transform.position;
        }

        private void FixedUpdate()
        {
            if (_character != null)
            {
                Vector3 desiredPosition = _character.transform.position + _offsetFromCharacter;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeedCamera);
                transform.position = smoothedPosition;
            }
        }
    }
}

using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class TestEnemyCircleMoving : MonoBehaviour
    {
        private Vector3 _startPosition;
        private float _offset = 2.5f;
        private float _speed;
    
        void Start()
        {
            _startPosition = transform.position;
        }

        void Update()
        {
            var xPos = Mathf.Sin(Time.time);
            var zPos = Mathf.Cos(Time.time);
            var position = new Vector3(_startPosition.x + xPos *_offset, _startPosition.y, _startPosition.z + zPos *_offset);
            transform.position = position;
        }
    }
}

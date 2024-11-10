using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AimingModule.Test
{
    public class TargetCaller : MonoBehaviour
    {
        private IAimingController _aimingController;
    
        public void Init(IAimingController aimingController)
        {
            _aimingController = aimingController;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _aimingController.GetTarget();
            }
        }
    }
}

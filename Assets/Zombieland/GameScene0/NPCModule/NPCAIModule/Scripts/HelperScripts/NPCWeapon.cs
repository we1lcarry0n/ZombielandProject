using System;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCWeapon : MonoBehaviour
    {
        public event Action OnSlotNumber1;
        public event Action OnSlotNumber2;
        public event Action OnSlotNumber3;
        public event Action OnSlotNumber4;

        public void Init(INPCAIController nPCAIController)
        {
            Invoke(nameof(SetDefaultWeapon), 5f);
        }

        private void SetDefaultWeapon()
        {
            OnSlotNumber1?.Invoke();
        }
    }
}
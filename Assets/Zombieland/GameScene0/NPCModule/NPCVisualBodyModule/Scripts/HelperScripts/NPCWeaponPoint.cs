using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCWeaponPoint : MonoBehaviour
    {
        public Transform LeftHandWeaponPoint;
        public Transform RightHandWeaponPoint;
        public Transform TeethWeaponPoint;
        public Transform LeftFoot;

        public Transform GetWeaponPoint(NPCVisualBodyController nPCVisualBodyController)
        {
            WeaponPoint weaponPoint = nPCVisualBodyController.NPCController.NPCDataController.NPCData.WeaponPoint;

            switch (weaponPoint)
            {
                case WeaponPoint.LeftHand:
                    return LeftHandWeaponPoint;
                    break;

                case WeaponPoint.RightHand:
                    return RightHandWeaponPoint;
                    break;

                case WeaponPoint.Teeth:
                    return TeethWeaponPoint;
                    break;

                default:
                    return LeftHandWeaponPoint;
                    break;
            }
        }
    }
}
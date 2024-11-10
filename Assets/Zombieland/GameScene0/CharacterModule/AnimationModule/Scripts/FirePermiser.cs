using Zombieland.GameScene0.ImpactModule;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class FirePermiser
    {
        private IAnimationController _animationController;

        public FirePermiser(IAnimationController animationController)
        {
            _animationController = animationController;
        }

        public bool CheckFirePermission(Weapon weapon)
        {
            // Test
            return true;

            bool isCheckResource = ResourcesConsumption();
            bool isDead = _animationController.CharacterController.CharacterDataController.CharacterData.IsDead;
            bool isStunned = _animationController.CharacterController.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                if (weapon.WeaponData.HasTarget)
                {
                    if (_animationController.CharacterController.AimingController.GetTarget() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ResourcesConsumption()
        {
            Impact impact = _animationController.CharacterController.RootController.GameDataController.GetData<Impact>(_animationController.CharacterController.EquipmentController.CurrentImpactID);

            bool isCheckResource = _animationController.CharacterController.EquipmentController.CurrentImpactCount >= 0;

            for (int i = 0; i < impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (impact.ImpactData.ConsumableResources[i].Value >= _animationController.CharacterController.CharacterDataController.CharacterData.Stamina)
                        {
                            isCheckResource = true;
                        }
                        else
                        {
                            isCheckResource = false;
                        }
                        break;

                    default:
                        isCheckResource = false;
                        break;
                }
            }

            return isCheckResource;
        }
    }
}
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        float RealMovingSpeed { get; set; }
        Vector2 DirectionWalk { get; set; }
        float RotationAngle { get; set; }  
        ICharacterController CharacterController { get; }
        void ActivateMoving(bool isActive);
    }
}
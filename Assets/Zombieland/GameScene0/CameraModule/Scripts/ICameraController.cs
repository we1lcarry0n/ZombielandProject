using Cinemachine;
using UnityEngine;

namespace Zombieland.GameScene0.CameraModule
{
    public interface ICameraController
    {
        Camera PlayerCamera { get; }
        CinemachineVirtualCamera CinemachineVirtualCamera { get; }
    }
}


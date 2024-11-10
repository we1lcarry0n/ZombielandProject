using Cinemachine;
using UnityEngine;

namespace Zombieland.GameScene0.CameraModule
{ 
    public class InitializerCamera
    {
        private const string PREFAB_NAME = "CameraCinemachine";
        public Camera MainCamera { get; private set; }
        public CinemachineVirtualCamera CinemachineVirtualCamera { get; private set; }
        public void Init(CameraData cameraData, Transform characterFollowTransform)
        {
            GameObject prefab = Resources.Load<GameObject>(PREFAB_NAME);
            GameObject cameraGO = GameObject.Instantiate(prefab);
            CinemachineVirtualCamera = cameraGO.GetComponentInChildren<CinemachineVirtualCamera>();
            MainCamera = cameraGO.GetComponentInChildren<Camera>();

            CinemachineVirtualCamera.Follow = characterFollowTransform;
            CinemachineVirtualCamera.LookAt = characterFollowTransform;


            /*CameraFollow cameraFollow = cameraGO.GetComponent<CameraFollow>();
            MainCamera = cameraFollow.MainCamera.GetComponent<Camera>();

            cameraFollow.SetCharacterTransform(characterFollowTransform);
            cameraFollow.CameraPivot0.position = characterFollowTransform.position;


            cameraFollow.CameraPivot0.rotation = Quaternion.Euler(0f, cameraData.CameraPivot0RotationY, 0f);
            cameraFollow.CameraPivot1.localPosition = new Vector3(0f,
                                                cameraData.CameraPivot1LocalPositionY, cameraData.CameraPivot1LocalPositionZ);
            cameraFollow.MainCamera.localRotation = Quaternion.Euler(cameraData.CameraLocalRotationX, 0f, 0f);
            cameraFollow.MainCamera.GetComponent<Camera>().orthographicSize = cameraData.CameraSize;*/
        }
    }
}


using System.Collections;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForCameraAndAttach());
    }

    IEnumerator WaitForCameraAndAttach()
    {
        float timeout = 5f;

        while (timeout > 0f)
        {
            GameObject camera = GameObject.Find("Main Camera");

            if (camera != null)
            {
                camera.AddComponent<MovingCamera>();
                yield break;
            }

            yield return null;
            timeout -= Time.deltaTime;
        }
    }
}

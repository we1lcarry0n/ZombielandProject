using UnityEngine;

public class SecurityCameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraStand;
    [SerializeField] private GameObject camera;
    [SerializeField] private float rotationSpeed;

    private float minAngle;
    private float maxAngle;
    private GameObject player = null;

    void Awake()
    {
        SetRotationRange();
    }

    void Update()
    {
        if (player == null)
        {
            CameraPatrol();
        }
        else
        {
            CameraFollow();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void CameraPatrol()
    {
        float rotationAngle = Mathf.PingPong(Time.time * rotationSpeed, maxAngle - minAngle) + minAngle;
        Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    private void CameraFollow()
    {
        Vector3 direction = player.transform.position - camera.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SetRotationRange()
    {      
        switch (cameraStand.transform.rotation.eulerAngles.y)
        {
            case 0:
                minAngle = -70f;
                maxAngle = 70f;
                break;
            case 90:
                minAngle = 20f;
                maxAngle = 160f;
                break;
            case 180:
                minAngle = 110f;
                maxAngle = 200f;
                break;
            case 270:
                minAngle = 200f;
                maxAngle = 340f;
                break;
        }
    }

}

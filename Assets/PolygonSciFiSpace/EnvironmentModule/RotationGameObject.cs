using UnityEngine;

public class RotationGameObject : MonoBehaviour
{
    [SerializeField] private GameObject prefabRotate;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private bool isRotateAxisX;
    [SerializeField] private bool isRotateAxisY;
    [SerializeField] private bool isRotateAxisZ;

    void Update()
    {
        RotateGameObject();
    }

    private void RotateGameObject()
    {
        float rotationSpeed = rotateSpeed * Time.deltaTime;
        if (isRotateAxisX)
        {
            prefabRotate.transform.Rotate(rotationSpeed, 0, 0);
        }
        if (isRotateAxisY)
        {
            prefabRotate.transform.Rotate(0, rotationSpeed, 0);
        }
        if (isRotateAxisZ)
        {
            prefabRotate.transform.Rotate(0, 0, rotationSpeed);
        }

    }
}

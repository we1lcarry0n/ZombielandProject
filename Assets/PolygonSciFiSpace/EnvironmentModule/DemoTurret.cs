using UnityEngine;

public class DemoTurret : MonoBehaviour
{
    // Префабы
    public GameObject floorRightPrefab;
    public GameObject floorLeftPrefab;
    public GameObject turretPrefab;
    public GameObject turretBarrelPrefab;
    public GameObject turretBasePrefab;
    public GameObject[] vfxPrefabs;

    // Поля для перемещения по X для двух частей пола
    public float floorMoveXRight;
    public float floorMoveXLeft;

    public float floorMoveXRightBack;
    public float floorMoveXLeftBack;


    // Скорость перемещения по X для двух частей пола
    public float floorMoveSpeed;

    // Поля для перемещения по Y для турели
    public float turretMoveY;

    // Скорость перемещения по Y для турели
    public float turretMoveSpeedY;

    // Скорость вращения ствола турели
    public float turretBarrelRotateSpeed;

    public Collider trigger1;
    private GameObject targetObject;

    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isMovingTurret = false;

    private bool isMovingRightBack = false;
    private bool isMovingLeftBack = false;

    private bool isRoRotateBarrel = false;

    public bool isTrigger1Enter = false;
    public bool isTrigger2Enter = false;

    public bool isTarget = false;

    void Start()
    {
        // Проверка, нужно ли двигаться вправо или влево
        if (floorRightPrefab.transform.position.x < floorMoveXRight)
            isMovingRight = true;
        if (floorLeftPrefab.transform.position.x > floorMoveXLeft)
            isMovingLeft = true;

        if (!isMovingTurret)
        {
            isMovingTurret = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") == trigger1)
        {           
                isTrigger1Enter = true;
            targetObject = other.gameObject;
        }

    }    

    void Update()
    {
        if (isTrigger1Enter)
        {
            // Если нужно двигаться вправо
            if (isMovingRight)
            {
                MoveFloorToPosition(floorRightPrefab.transform, floorMoveXRight);
            }
            // Если нужно двигаться влево
            if (isMovingLeft)
            {
                MoveFloorToPosition(floorLeftPrefab.transform, floorMoveXLeft);
            }
            // Если нужно двигаться вправо обратно
            if (isMovingRightBack)
            {
                MoveFloorToPositionBack(floorRightPrefab.transform, floorMoveXRightBack);
            }
            // Если нужно двигаться влево обратно
            if (isMovingLeftBack)
            {
                MoveFloorToPositionBack(floorLeftPrefab.transform, floorMoveXLeftBack);
            }
            // Двигаем турель
            if (isMovingTurret)
            {
                MoveTurretToPosition();
            }
            if (isTarget)
            {
                RotateTurretBaseTowards(targetObject);
            }
            
        }      
            
            if (isRoRotateBarrel)
            {
                RotateTurretBarrel();
            }

        
        
    }


    void RotateTurretBaseTowards(GameObject target)
    {
        if (turretBasePrefab == null || target == null)
        {
            Debug.LogWarning("Turret base prefab or target is not set!");
            return;
        }

        Vector3 targetDirection = target.transform.position - turretBasePrefab.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        turretBasePrefab.transform.rotation = targetRotation;
    }

    private void MoveFloorToPosition(Transform floor, float target)
    {
        float step = floorMoveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(target, floor.position.y, floor.position.z);
        floor.position = Vector3.MoveTowards(floor.position, targetPosition, step);

        // Проверяем, достигли ли целевой позиции
        if (Mathf.Abs(floor.position.x - target) < 0.01f)
        {
            // Останавливаем движение
            isMovingRight = false;
            isMovingLeft = false;            
        }
    }

    private void MoveTurretToPosition()
    {
        float step = turretMoveSpeedY * Time.deltaTime;
        Vector3 targetPosition = new Vector3(turretPrefab.transform.position.x, turretMoveY, turretPrefab.transform.position.z);
        turretPrefab.transform.position = Vector3.MoveTowards(turretPrefab.transform.position, targetPosition, step);

        // Проверяем, достигли ли целевой позиции
        if (Vector3.Distance(turretPrefab.transform.position, targetPosition) < 0.01f)
        {
            isMovingTurret = false;
            isMovingRightBack = true;
            isMovingLeftBack = true;
            isTarget = true;
            isRoRotateBarrel = true;
        }
    }

    private void MoveFloorToPositionBack(Transform floor, float target)
    {
        float step = floorMoveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(target, floor.position.y, floor.position.z);
        floor.position = Vector3.MoveTowards(floor.position, targetPosition, step);

        // Проверяем, достигли ли целевой позиции
        if (Mathf.Abs(floor.position.x - target) < 0.01f)
        {
            // Останавливаем движение
            isMovingRightBack = false;
            isMovingLeftBack = false;
            
        }
    }

    private void RotateTurretBarrel()
    {
        float rotationSpeed = turretBarrelRotateSpeed * Time.deltaTime;
        turretBarrelPrefab.transform.Rotate(0, 0, rotationSpeed);
        vfxPrefabs[0].SetActive(true);
        vfxPrefabs[1].SetActive(true);
    }

}

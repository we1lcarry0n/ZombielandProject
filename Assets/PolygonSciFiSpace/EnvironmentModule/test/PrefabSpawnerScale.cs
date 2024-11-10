using UnityEngine;

public class PrefabSpawnerScale : MonoBehaviour
{
    public GameObject prefab; // Префаб для спавна
    public Vector3 spawnArea; // Область спавна (X, Y и Z координаты)
    public int numberOfPrefabs; // Количество префабов для создания
    public Vector2 scaleRange; // Диапазон для случайного скейла по XYZ

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x),
                                                Random.Range(-spawnArea.y, spawnArea.y),
                                                Random.Range(-spawnArea.z, spawnArea.z));
            Quaternion spawnRotation = Quaternion.identity;

            GameObject newPrefab = Instantiate(prefab, spawnPosition, spawnRotation);

            // Генерация случайного масштаба по XYZ
            float scaleX = Random.Range(scaleRange.x, scaleRange.y);
            float scaleY = Random.Range(scaleRange.x, scaleRange.y);
            float scaleZ = Random.Range(scaleRange.x, scaleRange.y);
            newPrefab.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}

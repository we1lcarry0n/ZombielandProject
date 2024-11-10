using UnityEngine;

public class PrefabSpawnerNoScale : MonoBehaviour
{
    public GameObject prefab; // Префаб для спавна
    public Vector3 spawnArea; // Область спавна (X, Y и Z координаты)
    public int numberOfPrefabs; // Количество префабов для создания

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

            Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
}

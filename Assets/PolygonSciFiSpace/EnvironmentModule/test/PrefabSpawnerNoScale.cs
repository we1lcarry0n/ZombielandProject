using UnityEngine;

public class PrefabSpawnerNoScale : MonoBehaviour
{
    public GameObject prefab; // ������ ��� ������
    public Vector3 spawnArea; // ������� ������ (X, Y � Z ����������)
    public int numberOfPrefabs; // ���������� �������� ��� ��������

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

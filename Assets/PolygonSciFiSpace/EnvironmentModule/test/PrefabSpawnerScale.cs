using UnityEngine;

public class PrefabSpawnerScale : MonoBehaviour
{
    public GameObject prefab; // ������ ��� ������
    public Vector3 spawnArea; // ������� ������ (X, Y � Z ����������)
    public int numberOfPrefabs; // ���������� �������� ��� ��������
    public Vector2 scaleRange; // �������� ��� ���������� ������ �� XYZ

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

            // ��������� ���������� �������� �� XYZ
            float scaleX = Random.Range(scaleRange.x, scaleRange.y);
            float scaleY = Random.Range(scaleRange.x, scaleRange.y);
            float scaleZ = Random.Range(scaleRange.x, scaleRange.y);
            newPrefab.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}

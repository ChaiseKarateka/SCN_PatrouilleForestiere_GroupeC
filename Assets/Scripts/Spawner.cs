using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public BoxCollider spawnZone;
    public int spawnCount = 10;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        Bounds b = spawnZone.bounds;

        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        float z = Random.Range(b.min.z, b.max.z);

        Vector3 pos = new Vector3(x, y, z);

        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 randomRotation = new Vector3(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );

        Instantiate(prefab, pos, Quaternion.Euler(randomRotation));
    }
}
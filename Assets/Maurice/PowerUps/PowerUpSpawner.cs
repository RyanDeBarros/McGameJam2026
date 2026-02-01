using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject sprayPrefab;

    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    public float spawnY = 1f; // height where it spawns

    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            SpawnPowerUp();
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnSpray();
        }
    }

    void SpawnPowerUp()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
    void SpawnSpray()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        Instantiate(sprayPrefab, spawnPosition, Quaternion.identity);
    }
}

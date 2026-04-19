using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;

    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;

    private float nextSpawnTime;

    public float heightOffset = 3f;

    void Start()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        // spawn few clouds at beginning
        for (int i = 0; i < 5; i++)
        {
            SpawnCloudAtStart(i);
        }
    }

    void Update()
    {
        nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime <= 0)
        {
            SpawnCloud();
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnCloud()
    {
        // random height
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        float randomY = Random.Range(lowestPoint, highestPoint);

        // slight variation to avoid stacking
        randomY += Random.Range(-0.5f, 0.5f);

        // create cloud
        GameObject newCloud = Instantiate(
            cloudPrefab,
            new Vector3(transform.position.x, randomY, 0),
            Quaternion.identity
        );

        // random size
        float scale = Random.Range(0.7f, 1.3f);
        newCloud.transform.localScale = new Vector3(scale, scale, 1);
    }

    void SpawnCloudAtStart(int index)
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        float randomY = Random.Range(lowestPoint, highestPoint);

        float offsetX = index * 4f;

        GameObject newCloud = Instantiate(
            cloudPrefab,
            new Vector3(transform.position.x - offsetX, randomY, 0),
            Quaternion.identity
        );

        float scale = Random.Range(0.7f, 1.3f);
        newCloud.transform.localScale = Vector3.one * scale;
    }
}
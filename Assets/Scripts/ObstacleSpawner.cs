using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform player;
    public List<GameObject> obstaclePrefabs;

    public float spawnIntervalZ = 5f;
    public float initialSpawnDistance = 200f;
    public float maintainAheadDistance = 100f;
    public float laneXRange = 8.6f;

    private float nextSpawnZ;

    void Start()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Count == 0)
        {
            Debug.LogError("NO OBSTACLES ASSIGNED");
            return;
        }

        nextSpawnZ = player.position.z;

        float endZ = player.position.z + initialSpawnDistance;

        while (nextSpawnZ < endZ)
        {
            SpawnObstacle(nextSpawnZ);
            nextSpawnZ += spawnIntervalZ;
        }
    }

    void Update()
    {
        float targetZ = player.position.z + maintainAheadDistance;

        while (nextSpawnZ < targetZ)
        {
            SpawnObstacle(nextSpawnZ);
            nextSpawnZ += spawnIntervalZ;
        }

        Cleanup();
    }

    void SpawnObstacle(float z)
    {
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

        float x = Random.Range(-laneXRange, laneXRange);

        Instantiate(prefab, new Vector3(x, 0.25f, z), Quaternion.identity);
    }

    void Cleanup()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in objs)
        {
            if (obj.transform.position.z < player.position.z - 30f)
            {
                Destroy(obj);
            }
        }
    }
}
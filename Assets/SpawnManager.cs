using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject roadPrefab;
    public GameObject coinPrefab;
    public GameObject obstaclePrefab1; // slot for hurdle
    public GameObject obstaclePrefab2; // slot for tall wall
    public Transform playerTransform;

    [Header("Settings")]
    public float roadLength = 50f;
    public int numberOfRoads = 5;
    public float laneDistance = 3f;

    private float spawnZ = 0;
    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfRoads; i++)
        {
            SpawnRoad(i >= 2); 
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > spawnZ - (numberOfRoads * roadLength))
        {
            SpawnRoad(true);
            DeleteRoad();
        }
    }

    public void SpawnRoad(bool spawnItems)
    {
        GameObject go = Instantiate(roadPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeRoads.Add(go);
        
        if (spawnItems)
        {
            // 50/50 chance for obstacles vs coins
            if (Random.value > 0.5f) 
            {
                SpawnObstacle(spawnZ);
            } 
            else 
            {
                SpawnCoins(spawnZ);
            }
        }
        spawnZ += roadLength;
    }

    void SpawnObstacle(float zPos)
    {
        int lane = Random.Range(-1, 2);
        
        // 1. decide which obstacle to spawn
        GameObject obstacleToSpawn;
        if (Random.value > 0.5f) {
            obstacleToSpawn = obstaclePrefab1;
        } else {
            obstacleToSpawn = obstaclePrefab2;
        }

        // 2. spawn it at a random spot on the road
        Vector3 pos = new Vector3(lane * laneDistance, 0.5f, zPos + Random.Range(10f, 40f));
        Instantiate(obstacleToSpawn, pos, Quaternion.identity);
    }

    void SpawnCoins(float zPos)
    {
        int lane = Random.Range(-1, 2);
        float randomZOffset = Random.Range(10f, 30f);
        
        for (int i = 0; i < 5; i++)
        {
            float spacing = i * 2f;
            Vector3 coinPos = new Vector3(lane * laneDistance, 1f, zPos + randomZOffset + spacing);
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }

    void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
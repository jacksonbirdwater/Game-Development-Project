using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject[] edgeObstacles;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private bool hasSpawned = false;

    public void SpawnObstacles()
    {
        if (hasSpawned) return;

        int obstacleCount = Random.Range(1, 4);

        for (int i = 0; i < obstacleCount; i++)
        {
            bool spawnLeft = Random.Range(0, 2) == 0;

            Transform spawnPoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;

            GameObject obstacle = edgeObstacles[Random.Range(0, edgeObstacles.Length)];

            Quaternion rot = Quaternion.Euler(0, 180, 0);

            Instantiate(obstacle, spawnPoint.position, rot, transform);
        }

        hasSpawned = true;
    }
}
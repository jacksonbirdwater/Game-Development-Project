using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public Transform[] lanePoints;

    public float spawnDistance = 40f; // how far ahead of player to spawn
    public float rowSpacing = 20f;    // distance between rows
    public float spawnDelay = 0.25f;  // how fast rows spawn

    private float nextSpawnZ;
    private float timer;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistance;
    }

    void Update()
    {
        if (GameState.isGameOver) return;
        if (!GameState.isGameStarted) return;

        timer += Time.deltaTime;

        if (timer < spawnDelay) return;

        // ensure we always stay ahead of player
        float minZ = player.position.z + spawnDistance;

        if (nextSpawnZ < minZ)
            nextSpawnZ = minZ;

        SpawnRow();

        nextSpawnZ += rowSpacing;
        timer = 0f;
    }

    void SpawnRow()
    {
        if (lanePoints == null || lanePoints.Length == 0) return;

        int safeLane = Random.Range(0, lanePoints.Length);

        for (int i = 0; i < lanePoints.Length; i++)
        {
            if (i == safeLane) continue;

            if (Random.value < 0.9f)
            {
                Vector3 lane = lanePoints[i].position;

                Vector3 pos = new Vector3(
                    lane.x,
                    lane.y,
                    nextSpawnZ
                );

                Instantiate(obstaclePrefab, pos, Quaternion.identity);
            }
        }
    }
}
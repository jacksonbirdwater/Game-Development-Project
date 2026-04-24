using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;
    public Transform[] lanePoints;

    public float laneOffset = 0f;
    public float yOffset = 1f;

    public float spawnDistance = 40f;
    public float rowSpacing = 20f;
    public float spawnDelay = 0.3f;

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

        float minZ = player.position.z + spawnDistance;

        if (nextSpawnZ < minZ)
            nextSpawnZ = minZ;

        SpawnCoinRow();

        nextSpawnZ += rowSpacing;
        timer = 0f;
    }

    void SpawnCoinRow()
    {
        if (lanePoints == null || lanePoints.Length == 0) return;

        int laneIndex = Random.Range(0, lanePoints.Length);

        Vector3 lane = lanePoints[laneIndex].position;

        Vector3 pos = new Vector3(
            lane.x + laneOffset,
            yOffset,
            nextSpawnZ
        );

        Instantiate(coinPrefab, pos, Quaternion.identity);
    }
}
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public RoadSpawner roadSpawner;

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoads();
    }
}
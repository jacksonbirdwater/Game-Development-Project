using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered()
    {

        roadSpawner.MoveRoads();
    }
}

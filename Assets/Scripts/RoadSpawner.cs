using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    [SerializeField] private float offset = 71.66f;
    private bool ready = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }

        StartCoroutine(EnableAfterFrame());
    }

    IEnumerator EnableAfterFrame()
    {
        yield return null; // wait 1 frame
        ready = true;
    }


    public void MoveRoads()
    {
        if (!ready) return;

        GameObject movedRoad = roads[0];
        roads.RemoveAt(0);

        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        movedRoad.transform.position = new Vector3(0, 0, newZ);

        roads.Add(movedRoad);
    }
}

using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    private Transform player;

    private float yOffset = 5f;
    private float zOffset = -9f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(
            player.position.x,
            player.position.y + yOffset,
            player.position.z + zOffset
        );
    }
}
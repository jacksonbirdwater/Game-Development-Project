using UnityEngine;

public class FollowScript : MonoBehaviour
{
    // drag your player object into this slot in the inspector
    public Transform player;

    // this handles how far away the camera is
    // try (0, 5, -10) as a starting point
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate()
    {
        if (player == null) return;

        // 1. start with the player's current position
        Vector3 targetPosition = player.position + offset;

        // 2. lock the X position to 0 
        // this keeps the camera perfectly centered on the road
        targetPosition.x = 0;

        // 3. apply the position to the camera
        transform.position = targetPosition;

        // 4. optional: make the camera always look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
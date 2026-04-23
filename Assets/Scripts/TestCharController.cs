using UnityEngine;

public class TestCharController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;

    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float vMovement = Input.GetAxis("Vertical") * movementSpeed;

        transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
            return;
        }

        spawnManager.SpawnTriggerEntered();
    }
}
using UnityEngine;

public class Coin : MonoBehaviour
{
    // how fast the coin spins in the scene
    public float rotationSpeed = 100f;

    void Update()
    {
        // keep it rotating so the player notices it
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the object we hit is tagged as "player"
        if (other.CompareTag("Player"))
        {
            // add a point to our score manager
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(1);
            }

            // remove the coin so it disappears after being collected
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (GameState.isGameOver) return;

        if (collision.transform.root.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");

            GameManager.Instance.GameOver();
        }
    }
}
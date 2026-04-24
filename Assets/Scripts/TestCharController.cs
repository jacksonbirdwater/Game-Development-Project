using UnityEngine;

public class TestCharController : MonoBehaviour
{  
    public float movementSpeed = 12f;
    public float laneWidth = 2.5f;
    public float laneChangeSpeed = 12f;
    public float laneOffset = 0f;
    [Header("Speed Scaling")]
    public float baseSpeed = 12f;
    public float speedIncrease = 0.2f;
    public float maxSpeed = 25f;
    public float CurrentSpeed => currentSpeed;

    private float currentSpeed;

    private int currentLane = 1;
    private Rigidbody rb;

    public SpawnManager spawnManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        HandleInput();

        if (!GameState.isGameStarted || GameState.isGameOver) return;

        currentSpeed += speedIncrease * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentLane++;

        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    void MovePlayer()
    {
        float targetX = laneOffset + ((currentLane - 1) * laneWidth);

        Vector3 pos = rb.position;

        float newX = Mathf.Lerp(pos.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);

        Vector3 newPos = new Vector3(
            newX,
            pos.y,
            pos.z + currentSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameState.isGameOver) return;

        if (other.CompareTag("Obstacle"))
        {
            GameState.isGameOver = true;
            Debug.Log("Game Over");
            Time.timeScale = 0f;
            return;
        }

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();
            return;
        }
    }
}
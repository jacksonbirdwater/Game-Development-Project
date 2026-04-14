using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // needed to restart the game

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    
    [Header("Movement")]
    public float speed = 12f;
    public float laneDistance = 3f;
    public float laneSpeed = 20f;

    [Header("Physics")]
    public float jumpForce = 10f;
    public float gravity = -30f;
    public LayerMask groundLayer;

    private Vector3 velocity;
    private int desiredLane = 1; // 0:left, 1:middle, 2:right

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. set forward momentum
        velocity.z = speed;

        // 2. lane switching input
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            desiredLane = Mathf.Max(0, desiredLane - 1);
        
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            desiredLane = Mathf.Min(2, desiredLane + 1);

        // 3. calculate target x position
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        // 4. smooth lane movement (shaking fix included)
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = Vector3.zero;
        if (diff.sqrMagnitude > 0.005f) 
        {
            moveDir = diff.normalized * laneSpeed;
        }

        // 5. jumping logic
        bool isGrounded = Physics.CheckSphere(transform.position + Vector3.up * 0.1f, 0.15f, groundLayer);
        
        if (isGrounded)
        {
            velocity.y = -1f; 
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // 6. execute movement
        Vector3 finalMove = new Vector3(moveDir.x, velocity.y, velocity.z);
        controller.Move(finalMove * Time.deltaTime);
    }

    // 7. collision with obstacles
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            // restart the level if we hit an obstacle
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
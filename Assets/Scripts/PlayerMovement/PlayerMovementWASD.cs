using UnityEngine;
using Unity.Netcode;
public class PlayerMovementWASD : NetworkBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
        // Set Rigidbody to kinematic so gravity won't affect it (optional)
        rb.isKinematic = true;
    }

    void Update()
    {
        if(IsOwner)
        {
            Move();
        }
        
    }
    void Move()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A (-1), D (+1), Left, Right
        float moveZ = Input.GetAxis("Vertical");   // W (+1), S (-1), Up, Down

        // Create a movement vector
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;
        rb.MovePosition(transform.position + moveInput * moveSpeed * Time.deltaTime);
        // Move the player (in FixedUpdate for smoother physics)
    }
    void FixedUpdate()
    {
        // Apply movement
        
    }
}

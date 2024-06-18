using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    private Vector3 forceDirection;

    [SerializeField]
    private float jumpSpeed;
    public bool isWalking, isGrounded;
    public Rigidbody rb;
    private bool isClimbing;
    private Vector3 climbDirection;

    [SerializeField]
    private float gravity = 9.8f;
    private float climbSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            // float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0.0f, 0.0f).normalized;

            if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
                
            if (direction.magnitude >= 0.1f)
            {
                // Ensure the character faces the direction of movement
                transform.forward = direction;

                moveDirection = direction * maxSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            else
            {
                moveDirection.x = 0;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (isClimbing)
        {
            Climb();
        }
    }

    private void Climb()
    {
        rb.useGravity = false;
        climbDirection = new Vector3(0, Input.GetAxis("Vertical") * climbSpeed, 0);
        
        // Use CharacterController for smooth movement
        if (controller != null)
        {
            controller.Move(climbDirection * Time.deltaTime);
        }
        // Or use Rigidbody if you prefer physics-based movement
        else if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, climbDirection.y, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Start climbing when the player enters the ladder trigger
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop climbing when the player exits the ladder trigger
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.useGravity = true; 
        }
    }
}

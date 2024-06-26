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
    private float velocity = 3.0f;

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
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 climbDirection = new Vector3(0, verticalInput * climbSpeed, 0);
            controller.Move(climbDirection * Time.deltaTime);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        // Start climbing when the player enters the ladder trigger
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.useGravity = false; 
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

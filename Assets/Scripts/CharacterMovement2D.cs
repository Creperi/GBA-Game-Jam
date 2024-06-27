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

    [SerializeField]
    private float detectionRange = 2.0f;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isClimbing)
        {
            HandleClimbing();
        }
        else
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(horizontal, 0.0f, 0.0f).normalized;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetTrigger("Jump");
            }

            if (direction.magnitude >= 0.1f)
            {
                // Ensure the character faces the direction of movement
                transform.forward = direction;

                moveDirection = direction * maxSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    animator.SetTrigger("Jump");
                }

                animator.SetBool("isWalking", true);
            }
            else
            {
                moveDirection.x = 0;
                animator.SetBool("isWalking", false);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 climbDirection = new Vector3(0, verticalInput * climbSpeed, 0);
        controller.Move(climbDirection * Time.deltaTime);

        animator.SetBool("isClimbing", true);
        gameObject.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        if (Input.GetKeyDown(KeyCode.E))
        {
            isClimbing = false;
            rb.useGravity = true;
            animator.SetBool("isClimbing", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.useGravity = false; 
            animator.SetBool("isClimbing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.useGravity = true; 
            gameObject.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
            animator.SetBool("isClimbing", false);
        }
    }
}

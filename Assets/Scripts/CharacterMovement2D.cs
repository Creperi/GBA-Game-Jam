using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    private Vector2 forceDirection;

    [SerializeField]
    private float jumpSpeed;
    public bool isWalking, isGrounded;

    [SerializeField]
    private float gravity = 9.8f;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody rb;
    [SerializeField]
    private Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            playerAnimator.SetBool("isWalking", true);

            moveDirection.x = horizontal * maxSpeed;

            // Flip character sprite based on movement direction
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            moveDirection.x = 0;
            playerAnimator.SetBool("isWalking", false);
        }

        // Jumping
        if (isGrounded && Input.GetKey("space"))
        {
            moveDirection.y = jumpSpeed;
            isGrounded = false;
            playerAnimator.SetTrigger("jump");
        }

        // Apply gravity
        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        rb.velocity = new Vector2(moveDirection.x, rb.velocity.y);

        // Detect if grounded
        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
    }
}

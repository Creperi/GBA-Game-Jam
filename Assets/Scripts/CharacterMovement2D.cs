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

    [SerializeField]
    private float gravity = 9.8f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
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
    private float rotationSpeed = 500.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Calculate movement direction
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                moveDirection = transform.forward * maxSpeed * direction.magnitude;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            else
            {
                moveDirection.x = 0;
                moveDirection.z = 0;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }



}

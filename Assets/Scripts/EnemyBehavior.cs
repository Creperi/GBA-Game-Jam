using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private Rigidbody rb;
    private Vector2 movement;

    public Animator EnemyAnimator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EnemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = new Vector2(direction.x, direction.y);
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); // Face left
        }
        else if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Face right
        }
    }

    void FixedUpdate()
    {
        // Move the enemy towards the player if not in attack range
        if (!EnemyAnimator.GetBool("isAttacking"))
        {
            MoveEnemy(movement);
        }
    }


    void MoveEnemy(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, 0.0f, direction.y);
        rb.MovePosition(transform.position + move * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            EnemyAnimator.SetBool("isAttacking", true);
        }
        if(other.CompareTag("Hammer")){
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            EnemyAnimator.SetBool("isAttacking", false);
        }
    }

}

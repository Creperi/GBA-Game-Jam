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
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
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
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            EnemyAnimator.SetBool("isAttacking", true);
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            EnemyAnimator.SetBool("isAttacking", false);
        }
    }

}

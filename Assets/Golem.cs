using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{

    public bool isPatroling = true;
    public Rigidbody2D rb;
    public float walkSpeed;
    public bool isMoving = true;

    Collider2D golemCollider;
    Animator animator;
    bool isAlive = true;
    //bool isAttacking = false;
    public float Health {
        set {
            // If golem looses health set trigger for hurt animation
            if (value < health) {
                animator.SetTrigger("hit");
            }

            health = value;
          
            // If golem dies
            if(health <= 0) {
                animator.SetBool("isAlive", false);
                isMoving = false;
            }
        }
        get {
            return health;
        }
    }

    public void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        golemCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        //isPatroling = true;
        animator.SetBool("isMoving", true);
    }

    void FixedUpdate() {
       if (isPatroling) {
            Patroling();
        }
    }

    void Patroling() {
        if (isMoving) {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }
    
   
    public float health = 3;
   
    void OnHit(float damage) {
        Health -= damage;
    }
    void StartAttacking(float damage) {
        //isAttacking = true;
        Health -= damage;
    }
    void DespawnGolem() {
        Destroy(gameObject);
    }

    void DestroyGolemCollider() {
        Destroy(golemCollider);
    }

    void OnCollisionEnter2D(Collision2D col) {
        isPatroling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        isPatroling = true;     
    }
}

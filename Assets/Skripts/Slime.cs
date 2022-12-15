using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float SlimeDamage = 1f;

    [SerializeField]
    DetectionZone detectionZone;

    public float walkSpeed = 1f;
    public int health = 1;  
    public Rigidbody2D rb;

    private float collisionTimer = 0f;
    Animator animator;

    public int Health {
        set {
            health = value;
            // Slime dies 
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
        get {
            return health;
        }
    }

    public void Start() {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // If in agrozone follow player and set attack triggers
        if (detectionZone.detectedObjs.Count > 0) {
            // Calculate direction to target object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            animator.SetBool("isMoving", true);           
            //animator.SetBool("isAttacking", true);
            // Move towards detected object
            if (direction.x > 0) {
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else if (direction.x < 0) {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            rb.AddForce(direction * walkSpeed * Time.fixedDeltaTime);
        } else {
            animator.SetBool("isMoving", false);
        }
    }
  
    void OnCollisionStay2D(Collision2D col) {
        // Update the collision timer
        collisionTimer += Time.deltaTime;

        // If the timer is greater than or equal to 0.75 second, take damage and reset the timer
        if (collisionTimer >= 0.75f) {
            col.collider.SendMessage("OnHit", SlimeDamage);
            print($"{this.gameObject.name} applied damage to {col.gameObject.name}");
            collisionTimer = 0f;
        }
    }

    void OnHit(int damage) {
        Health -= damage;
    }
}

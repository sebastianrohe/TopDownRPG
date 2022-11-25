using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool IsMoving {
        set {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    //
    public float moveSpeed = 2f;
    public float maxSpeed = 2f;

    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    public float idleFriction = 0.9f;

    public GameObject hammerHitbox;
    Collider2D hammerCollider;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;

    bool isMoving = false;
    bool canMove = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hammerCollider = hammerHitbox.GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);
    }

    void FixedUpdate() {

        if (canMove == true && moveInput != Vector2.zero) {
            // Move animation and add velocity

            // Accelerate the player while run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);
            //rb.velocity = new Vector3(moveSpeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.x);


            // Control whether looking left or right
            if (moveInput.x > 0) {
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else if (moveInput.x < 0) {
                //transform.eulerAngles = new Vector3(0, 180, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                //transform.position = new Vector3(-0.4f, 0, 0);
            }
            IsMoving = true;
        } else {
            // No movement so interpolate velocity towards 0
            rb.velocity = Vector2.zero;
            IsMoving = false;
        }
    }
    bool isAlive = true;

    public float Health {
        set {

            if (value < health) {
                animator.SetTrigger("hit");
            }

            health = value;

            if (health <= 0) {
                animator.SetBool("isAlive", false);
            }
        }
        get {
            return health;
        }
    }

    public float health = 3;

    void OnHit(float damage) {
        Health -= damage;
        //Debug.LogWarning("Golem hit" + damage);
    }
    // Get input values for player movement
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("HammerAttack");
    }

    void LockMovement() {
        canMove = false;
    }

    void UnlockMovement() {
        canMove = true;
    }
}

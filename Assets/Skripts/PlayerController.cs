using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float moveSpeed = 2f;
    public float maxSpeed = 2f;

    // Speed of the player's dash
    public float dashSpeed = 10.0f;


    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    // public float idleFriction = 0.9f;

    public GameObject hammerHitbox;
    public float health = 3;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Collider2D playerCollider;

    Collider2D hammerCollider;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;

    bool isMoving = false;
    bool canMove = true;
    bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hammerCollider = hammerHitbox.GetComponent<Collider2D>();
        playerCollider = playerCollider.GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);
    }


    void FixedUpdate()
    {
        if (canMove == true && moveInput != Vector2.zero)
        {
            // Move animation and add velocity
            // Accelerate the player while run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);
            //rb.velocity = new Vector3(moveSpeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.x);
            //rb.AddForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
            // Control whether looking left or right
            if (moveInput.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (moveInput.x < 0)
            {
                //transform.eulerAngles = new Vector3(0, 180, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                //transform.position = new Vector3(-0.4f, 0, 0);
            }
            IsMoving = true;
        }
        else
        {
            // No movement so interpolate velocity towards 0
            rb.velocity = Vector2.zero;
            IsMoving = false;
        }

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        /*// Check if the player pressed the E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Calculate the direction and distance of the dash
            Vector3 dashDirection = transform.forward;
            float dashDistance = dashSpeed * Time.deltaTime;

            // Move the player in the dash direction by the dash distance
            transform.position += dashDirection * dashDistance;
        }*/
    }


    public float Health
    {
        set
        {

            if (value < health)
            {
                animator.SetTrigger("hit");
            }

            health = value;

            if (health <= 0)
            {
                animator.SetBool("isAlive", false);
            }
        }
        get
        {
            return health;
        }
    }

    void OnHit(float damage)
    {
        Health -= damage;
    }
    // Get input values for player movement
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("HammerAttack");

    }
    void LockMovement()
    {
        canMove = false;
    }


    void UnlockMovement()
    {
        canMove = true;
    }

    void DestroyPlayerCollider()
    {
        Destroy(playerCollider);
    }

    void DespawnPlayer()
    {
        Destroy(gameObject);
    }
}

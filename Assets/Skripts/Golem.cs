using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Golem : MonoBehaviour {
    [SerializeField]
    DetectionZone detectionZone;

    [SerializeField]
    AttackingZone attackingZone;

    [SerializeField]
    GameObject golemAttackHitbox;

    [SerializeField]
    private int golemExpValue = 50;

    Collider2D golemCollider;
    Animator animator;

    public Rigidbody2D rb;

    public float walkSpeed = 1f;
    public int health = 3;

    public Image[] HPBar;
    public Sprite fullHP;
    public Sprite emptyHP;

    bool isAlive = true;
    bool isAttacking = false;
    public bool isPatroling = true;
    public bool isMoving = false;

    public int Health {
        set {
            // If golem looses health set trigger for hurt animation
            if (value < health) {
                animator.SetTrigger("hit");
            }

            health = value;

            // If golem dies
            if (health <= 0) {
                //Destroy(golemCollider);
                animator.SetBool("isAlive", false);
                isAlive = false; // Set bool to false to stop following player when golem is dead
                isMoving = false; // Set bool to false to stop moving in general when golem is dead
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
        animator.SetBool("isMoving", false);
    }

    void FixedUpdate() {
        // If in agrozone follow player and set attack triggers
        if (isAlive && detectionZone.detectedObjs.Count > 0) {
            // Calculate direction to target object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            animator.SetBool("isMoving", true);
            //animator.SetBool("isAttacking", true);
            if (attackingZone.detectedObjs.Count > 0) {
                animator.SetBool("isAttacking", true);
            }
            if (attackingZone.detectedObjs.Count == 0) {
                animator.SetBool("isAttacking", false);
            }
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
   
    void OnHit(int damage) {
        HPBar[health].enabled = false;
        Health -= damage;

        if(health >= 0) HPBar[health].enabled = true;
    }
    void StartAttacking(int damage) {
        //isAttacking = true;
        Health -= damage;
    }
    void DespawnGolem() {
        GameObject.Find("ExpBar").GetComponent<PlayerExperience>().CurrentExperience += golemExpValue;
        Destroy(gameObject);
    }

    void DestroyGolemCollider() {
        Destroy(golemCollider);
    }

    //TODO: Patrol
    //void Patroling() {
    //    if (isMoving) {
    //        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    //    }
    //}
}

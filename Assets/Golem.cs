using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    Animator animator;
    bool isAlive = true;

    public float Health {
        set {

            if (value < health) {
                animator.SetTrigger("hit");
            }

            health = value;
          
            if(health <= 0) {
                animator.SetBool("isAlive", false);              
            }
        }
        get {
            return health;
        }
    }

    public void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
    }

    public float health = 3;

    void OnHit(float damage) {
        Health -= damage;
        //Debug.LogWarning("Golem hit" + damage);
    }

    void DespawnGolem() {
        Destroy(gameObject);
    }
}

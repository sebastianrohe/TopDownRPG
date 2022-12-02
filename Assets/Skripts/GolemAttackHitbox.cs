using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackHitbox : MonoBehaviour {
    public Collider2D golemAttackCollider;
    public float GolemDamage = 1f;

    // Start is called before the first frame update
    private void Start() {
        // hammerCollider.GetComponent<Collider2D>();
        if (golemAttackCollider == null) {
            Debug.LogWarning("Golem collider not set");
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && col != null) {
            col.collider.SendMessage("OnHit", GolemDamage);
            print($"{this.gameObject.name} applied damage to {col.gameObject.name}");
        }
    }
}

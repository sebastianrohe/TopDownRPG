using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitbox : MonoBehaviour {
    public Collider2D hammerCollider;
    public float hammerDamage = 1f;

    private void Start() {
        // hammerCollider.GetComponent<Collider2D>();
        if (hammerCollider == null) {
            Debug.LogWarning("Hammer collider not set");
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        col.collider.SendMessage("OnHit", hammerDamage);
    }

}

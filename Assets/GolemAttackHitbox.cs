using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackHitbox : MonoBehaviour
{
    public float timer;
    public Collider2D golemAttackCollider;
    public float GolemDamage = 1f;
    // Start is called before the first frame update
    private void Start() {
        // hammerCollider.GetComponent<Collider2D>();
        if (golemAttackCollider == null) {
            Debug.LogWarning("Hammer collider not set");
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        timer += Time.deltaTime;
        if (timer >= 0.75)
        {
            col.collider.SendMessage("OnHit", GolemDamage);
            timer = 0;
        }        
    }
}

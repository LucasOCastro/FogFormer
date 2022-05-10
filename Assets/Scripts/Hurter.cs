using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FogFormer
{
    public class Hurter : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float knockback;
        [SerializeField] private float stun;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var health = collision.collider.GetComponent<HealthManager>();
            if (health == null)
            {
                return;
            }
            //TEMP VERY TEMP EWW
            health.Damage(damage);
            if (knockback != 0 && health.TryGetComponent<Rigidbody2D>(out var rb) && health.TryGetComponent<StatusManager>(out var status))
            {
                float xDif = Mathf.Sign(rb.position.x - transform.position.x);
                //EVEN MORE TEMP
                Vector2 dir = new Vector2(xDif, 1).normalized;
                rb.AddForce(dir * knockback, ForceMode2D.Impulse);
                status.Stun(stun);
            }
        }
    }    
}


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
        [Range(0,80)]
        [SerializeField] private float knockbackAngle;
        [SerializeField] private float stun;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var health = col.GetComponent<HealthManager>();
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
                float radAngle = Mathf.Deg2Rad * knockbackAngle;
                Vector2 dir = new Vector2(Mathf.Cos(radAngle) * xDif, Mathf.Sin(radAngle));
                rb.velocity = Vector2.zero;
                rb.AddForce(dir * knockback, ForceMode2D.Impulse);
                status.Stun(stun);
            }
        }
    }    
}


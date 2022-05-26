using System;
using UnityEngine;

namespace FogFormer
{
    [Serializable]
    public struct DamageData
    {
        [SerializeField] private int damage;
        public int Damage => damage;
        
        [SerializeField] private float knockback;
        public float Knockback => knockback;
        
        [Range(0,90)]
        [SerializeField] private float knockbackAngle;
        public float KnockbackAngle => knockbackAngle;
        
        [SerializeField] private float stunSeconds;
        public float StunSeconds => stunSeconds;

        public void ApplyDamage(HealthManager health, Transform damager)
        {
            //TEMP VERY TEMP EWW
            health.Damage(damage);

            if (stunSeconds > 0 && health.TryGetComponent<StatusManager>(out var status))
            {
                status.Stun(stunSeconds);
            }
            
            if (knockback != 0 && health.TryGetComponent<Rigidbody2D>(out var rb) )
            {
                float xDif = Mathf.Sign(rb.position.x - damager.position.x);
                //EVEN MORE TEMP
                float radAngle = Mathf.Deg2Rad * knockbackAngle;
                Vector2 dir = new Vector2(Mathf.Cos(radAngle) * xDif, Mathf.Sin(radAngle));
                rb.velocity = dir * knockback;
            }
        }
    }
}
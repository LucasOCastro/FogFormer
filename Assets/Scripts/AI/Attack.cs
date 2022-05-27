using System;
using System.Linq;
using FogFormer;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Collider2D))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private string animationName;
        [SerializeField] private DamageData damageData;
        [SerializeField] private Transform attackOrigin;
        [SerializeField] private bool faceTarget = true;

        private int _animationHash;

        [SerializeField] private Collider2D[] detectionColliders;
        private void Awake()
        {
            _animationHash = Animator.StringToHash(animationName);

            if (attackOrigin == null)
            {
                attackOrigin = transform;
            }
        }

        public bool WouldHit(Collider2D targetCol)
        {
            return detectionColliders.Any(attackCol => attackCol.IsTouching(targetCol));
        }

        private bool _attacking;
        public void Trigger(Animator animator, Transform target)
        {
            _attacking = true;
            if (animator != null)
            {
                animator.Play(_animationHash);    
            }
            if (faceTarget && target != null && animator.TryGetComponent(out DirectionManager flipper))
            {
                flipper.Face(target.position);
            }
        }

        public void End()
        {
            _attacking = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_attacking) return;

            HealthManager colHealth = col.GetComponent<HealthManager>();
            if (!colHealth) return;
            
            damageData.ApplyDamage(colHealth, attackOrigin);
        }

        
    }
}
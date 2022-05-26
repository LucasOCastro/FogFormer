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
        
        public HealthManager CurrentTarget => _currentTarget;

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

        private HealthManager _currentTarget;
        public void Trigger(HealthManager target, Animator animator)
        {
            _currentTarget = target;
            animator.Play(_animationHash);
            if (faceTarget && animator.TryGetComponent(out AIFlipper flipper))
            {
                flipper.Face(target.transform.position);
            }
        }

        public void End()
        {
            _currentTarget = null;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_currentTarget == null) return;

            HealthManager colHealth = col.GetComponent<HealthManager>();
            if (!colHealth || colHealth != _currentTarget) return;
            
            damageData.ApplyDamage(colHealth, attackOrigin);
        }

        
    }
}
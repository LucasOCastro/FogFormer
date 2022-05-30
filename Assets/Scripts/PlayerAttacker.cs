using System;
using UnityEngine;

namespace FogFormer
{
    public class PlayerAttacker : MonoBehaviour, IStunnable
    {
        [SerializeField] private string attackButton = "Attack";
        [SerializeField] private Attack attack;

        public Action OnAttack;

        public bool IsStunned { get; set; }
        
        public bool IsAttacking { get; private set; }

        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            HealthManager health = GetComponent<HealthManager>();
            health.OnDamage += OnAttackEnd;
        }

        private void Update()
        {
            if (IsStunned) return;
            
            if (Input.GetButtonDown(attackButton))
            {
                Attack();
            }
        }

        //AnimatorEvent
        private void OnAttackEnd()
        {
            if (!IsAttacking) return;
            attack.End();
            IsAttacking = false;
        } 

        private void Attack()
        {
            IsAttacking = true;
            OnAttack?.Invoke();
            attack.Trigger(null, null);
        }
    }
}
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
        
        private void Update()
        {
            if (IsStunned) return;
            
            if (Input.GetButtonDown(attackButton))
            {
                Attack();    
            }
        }

        //AnimatorEvent
        private void OnAttackEnd() => attack.End();

        private void Attack()
        {
            OnAttack?.Invoke();
            attack.Trigger(null, null);
        }
    }
}
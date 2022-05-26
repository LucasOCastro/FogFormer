using System.Linq;
using UnityEngine;

namespace FogFormer
{
    public class AIAttacker : MonoBehaviour, IStunnable
    {
        //TODO cooldowns?
        [SerializeField] private Attack[] attacks;
        [SerializeField] private Animator animator;
        
        public bool IsStunned { get; set; }

        public System.Action<Attack> OnAttackEnd;
        private Attack _currentAttack;
        public bool IsAttacking => _currentAttack != null;

        public bool HasValidAttackFor(HealthManager target) => FindValidAttack(target.GetComponent<Collider2D>()) != null;
        private Attack FindValidAttack(Collider2D col) => IsStunned ? null : attacks.FirstOrDefault(attack => attack.WouldHit(col));
        public bool TryTriggerValidAttack(HealthManager target)
        {
            if (IsAttacking) return false;
            
            Collider2D targetCol = target.GetComponent<Collider2D>();
            Attack attack = FindValidAttack(targetCol);
            if (attack == null)
            {
                return false;
            }
            
            attack.Trigger(animator, target.transform);
            _currentAttack = attack;
            return true;
        }

        private void EndAttack()
        {
            OnAttackEnd?.Invoke(_currentAttack);
            _currentAttack.End();
            _currentAttack = null;
        }
    }
}
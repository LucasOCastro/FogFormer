using System;
using UnityEngine;

namespace FogFormer
{
    public class HealthManager : MonoBehaviour
    {
        public Action OnDamage;
        public Action OnHeal;
        public Action OnDeath;
        
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
        
        private int _curHealth;
        public int Health
        {
            get => _curHealth; 
            private set
            {
                int newHealth = Mathf.Clamp(value, 0, maxHealth);
                if (newHealth == _curHealth) return;
                
                _curHealth = newHealth;
                if (_curHealth == 0)
                {
                    Death();
                }
            }
        }

        private void Awake()
        {
            _curHealth = maxHealth;
        }

        //TEMPP
        public void Damage(int damage)
        {
            Health -= damage;
            OnDamage?.Invoke();
        }

        private void Death()
        {
            //TEMP
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        }

        public void Heal(int healAmount)
        {
            Health += healAmount;
            OnHeal?.Invoke();
        }
    }
}
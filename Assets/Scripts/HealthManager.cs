using System;
using UnityEngine;

namespace FogFormer
{
    public class HealthManager : MonoBehaviour
    {
        public delegate void HealthUpdateEvent(int before, int after);
        public HealthUpdateEvent OnDamage;
        
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
        
        private int _curHealth;
        public int Health
        {
            get => _curHealth; 
            private set
            {
                _curHealth = Mathf.Clamp(value, 0, maxHealth);
                if (_curHealth == 0)
                {
                    Die();
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
            int beforeHealth = _curHealth;
            Health -= damage;
            OnDamage?.Invoke(beforeHealth, Health);
        }

        private void Die()
        {
            _curHealth = 0;
            //TEMP
            Destroy(gameObject);
        }
    }
}
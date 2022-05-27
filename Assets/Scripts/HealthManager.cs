using System;
using UnityEngine;

namespace FogFormer
{
    public class HealthManager : MonoBehaviour
    {
        public Action OnDamage;
        
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
            Health -= damage;
            OnDamage?.Invoke();
        }

        private void Die()
        {
            _curHealth = 0;
            //TEMP
            gameObject.SetActive(false);
        }
    }
}
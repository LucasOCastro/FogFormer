using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FogFormer
{
    public class Hurter : MonoBehaviour
    {
        [SerializeField] private DamageData damageData;
        [SerializeField] private float cooldown;

        private float _cooldownTimer;

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_cooldownTimer < cooldown)
            {
                return;
            }
            
            var health = col.GetComponent<HealthManager>();
            if (health != null)
            {
                _cooldownTimer = 0;
                damageData.ApplyDamage(health, transform);
            }
        }
    }    
}


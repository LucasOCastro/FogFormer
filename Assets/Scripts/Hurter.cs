using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FogFormer
{
    public class Hurter : MonoBehaviour
    {
        [SerializeField] private DamageData damageData;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var health = col.GetComponent<HealthManager>();
            if (health != null)
            {
                damageData.ApplyDamage(health, transform);
            }
        }
    }    
}


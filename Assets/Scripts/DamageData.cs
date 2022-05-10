using System;
using UnityEngine;

namespace FogFormer
{
    [Serializable]
    public struct DamageData
    {
        [SerializeField] private int damage;
        public int Damage => damage;
        [SerializeField] private float knockback;
        public float Knockback => knockback;
        [SerializeField] private float stunSeconds;
        public float StunSeconds => stunSeconds;
    }
}
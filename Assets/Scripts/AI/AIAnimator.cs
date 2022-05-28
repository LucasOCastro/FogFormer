using System;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Animator))]
    public class AIAnimator : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("walking");
        private static readonly int Hurt = Animator.StringToHash("hurt");

        private Mover _mover;
        private HealthManager _health;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();

            _health = GetComponent<HealthManager>();
        }

        private void Update()
        {
            if (!_mover) return;
            
            _animator.SetBool(Walking, _mover.IsMoving);
        }

        private void OnDamaged() => _animator.SetTrigger(Hurt);

        private void OnEnable()
        {
            if (_health != null)
                _health.OnDamage += OnDamaged;
        }
        private void OnDisable()
        {
            if (_health != null)
                _health.OnDamage -= OnDamaged;
        }
    }
}
using System;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Animator))]
    public class AIAnimator : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("walking");

        private Mover _mover;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            if (_mover)
            {
                _lastDirection = _mover.Direction;
            }
        }

        private int _lastDirection;
        private void Update()
        {
            if (!_mover) return;
            
            _animator.SetBool(Walking, _mover.IsMoving);
        }
    }
}
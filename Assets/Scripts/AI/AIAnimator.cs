using System;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Animator))]
    public class AIAnimator : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("walking");

        [SerializeField] private Mover mover;
        
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (mover)
            {
                _lastDirection = mover.Direction;
            }
        }

        private int _lastDirection;
        private void Update()
        {
            if (!mover) return;
            
            _animator.SetBool(Walking, mover.IsMoving);
        }
    }
}
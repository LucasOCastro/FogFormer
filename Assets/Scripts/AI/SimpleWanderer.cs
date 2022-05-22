using System;
using UnityEngine;

namespace FogFormer.AI
{
    public class SimpleWanderer : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("walking");

        [SerializeField] private Transform wanderTargetA, wanderTargetB;
        [SerializeField] private float speed;
        [SerializeField] private float restSeconds;
        //TODO TEMP
        [SerializeField] private Animator animator;

        private Transform _currentTarget;
        private float _restTimer;
        private void Awake()
        {
            _currentTarget = wanderTargetA;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, speed * Time.deltaTime);
            if (transform.position == _currentTarget.position)
            {
                SwitchTarget();
            }
        }

        private void SwitchTarget()
        {
            if (_restTimer < restSeconds)
            {
                animator.SetBool(Walking, false);
                _restTimer += Time.deltaTime;
                return;
            }
            animator.SetBool(Walking, true);
            _restTimer = 0;
            _currentTarget = (_currentTarget == wanderTargetA) ? wanderTargetB : wanderTargetA;
        }
    }
}
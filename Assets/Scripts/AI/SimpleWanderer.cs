using System;
using UnityEngine;

namespace FogFormer.AI
{
    public class SimpleWanderer : MonoBehaviour
    {
        [SerializeField] private Transform wanderTargetA, wanderTargetB;
        [SerializeField] private float speed;

        private Transform _currentTarget;

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
            _currentTarget = (_currentTarget == wanderTargetA) ? wanderTargetB : wanderTargetA;
        }
    }
}
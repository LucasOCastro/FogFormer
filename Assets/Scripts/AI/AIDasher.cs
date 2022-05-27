using System;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(DirectionManager))]
    public class AIDasher : MonoBehaviour
    {
        private DirectionManager _flipper;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _flipper = GetComponent<DirectionManager>();
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void DashForwards(float dashForce)
        {
            Vector2 dash = Vector2.right * _flipper.Direction * dashForce;
            Dash(dash);
        }

        public void DashBack(float dashForce)
        {
            Vector2 dash = Vector2.left * _flipper.Direction * dashForce;
            Dash(dash);
        }

        private void Dash(Vector2 dash) => _rb.AddForce(dash, ForceMode2D.Impulse);
    }
}
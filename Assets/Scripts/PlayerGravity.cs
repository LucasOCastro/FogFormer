using System;
using UnityEngine;

namespace FogFormer
{
    public class PlayerGravity : MonoBehaviour
    {
        [SerializeField] private string jumpButton = "Jump";
        [SerializeField] private float groundGravity;
        [SerializeField] private float fallGravity;
        [SerializeField] private float jumpGravity;
        [SerializeField] private float longJumpGravity;
        
        private Rigidbody2D _rb;
        private GroundedController _grounded;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _grounded = GetComponent<GroundedController>();
        }

        private void FixedUpdate()
        {
            _rb.gravityScale = GetGravityScale();
        }

        private float GetGravityScale()
        {
            if (_grounded.IsGrounded)
            {
                return groundGravity;
            }
            if (_rb.velocity.y <= 0) {
                return fallGravity;
            }
            return Input.GetButton(jumpButton) ? longJumpGravity : jumpGravity;
        }

    }
}
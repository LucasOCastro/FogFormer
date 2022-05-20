using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private float airAcceleration;
        [SerializeField] private float airDeceleration;
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";

        private Rigidbody2D _rb;
        private GroundedController _grounded;

        public Vector2 MoveInput { get; private set; }
        //Mathf.Sign is dumb
        public int InputDirection => MoveInput.x < 0 ? -1 : (MoveInput.x > 0 ? 1 : 0);
        public int LookDirection { get; private set; } = 1;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _grounded = GetComponent<GroundedController>();
        }

        private void Update()
        {
            MoveInput = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            if (MoveInput.x != 0) LookDirection = InputDirection;
        }

        private float VelocityDelta
        {
            get
            {
                if (Mathf.Abs(MoveInput.x) > 0.1f)
                {
                    return _grounded.IsGrounded ? acceleration : airAcceleration;
                }
                return _grounded.IsGrounded ? deceleration : airDeceleration;
            }
        }

        private void LateUpdate()
        {
            float velocityX = Mathf.MoveTowards(_rb.velocity.x, speed * MoveInput.x, VelocityDelta);
            if (!_grounded.IsGrounded)
            {
                _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
                return;
            }
            _rb.velocity = -(_grounded.GroundSlope) * velocityX;
            Debug.DrawRay(GetComponent<Collider2D>().bounds.CenterBottom(), -(_grounded.GroundSlope) * velocityX, Color.yellow);
        }
    }
    
}

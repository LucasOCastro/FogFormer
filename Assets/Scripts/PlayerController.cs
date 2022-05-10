using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float airAcceleration;
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";

        private Rigidbody2D _rb;

        public Vector2 MoveInput { get; private set; }
        //Mathf.Sign is dumb
        public int InputDirection => MoveInput.x < 0 ? -1 : (MoveInput.x > 0 ? 1 : 0);

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            MoveInput = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
        }

        private void FixedUpdate()
        {
            float velocityY = _rb.velocity.y;
            if (MoveInput.x != 0 || velocityY == 0)
            {
                float currentAcceleration = (velocityY != 0) ? airAcceleration : this.acceleration;
                float velocityX = Mathf.MoveTowards(_rb.velocity.x, MoveInput.x * speed, currentAcceleration);
                _rb.velocity = new Vector2(velocityX, velocityY);
            }
        }
    }
    
}

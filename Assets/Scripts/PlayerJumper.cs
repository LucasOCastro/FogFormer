using System.Reflection;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(GroundedController))]
    public class PlayerJumper : MonoBehaviour, IStunnable
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpQueueSeconds;
        [SerializeField] private string jumpButton = "Jump";

        public bool IsStunned { get; set; }

        private PlayerController _controller;
        private GroundedController _grounded;
        private bool _jumpQueued;
        private float _jumpQueueTimer;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _grounded = GetComponent<GroundedController>();
        }
        
        private void Update()
        {
            _jumpQueueTimer += Time.deltaTime;
            if (Input.GetButtonDown(jumpButton))
            {
                _jumpQueued = true;
                _jumpQueueTimer = 0;
            }
        }

        private void Jump()
        {
            _controller.SetVelocity(new Vector2(_controller.Velocity.x, jumpForce));
            _jumpQueued = false;
        }

        private bool CanJump() => !IsStunned && _jumpQueued && _jumpQueueTimer < jumpQueueSeconds && _grounded.IsGrounded;
        private void FixedUpdate()
        {
            if (CanJump())
            {
                Jump();
            }
        }
    }
}
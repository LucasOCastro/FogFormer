using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(GroundedController))]
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpQueueSeconds;
        [SerializeField] private string jumpButton = "Jump";

        [Header("Gravity")] 
        [SerializeField] private float gravity;
        [SerializeField] private float jumpGravity;
        [SerializeField] private float longJumpGravity;

        private Rigidbody2D _rb;
        private GroundedController _grounded;
        private bool _jumpQueued;
        private float _jumpQueueTimer;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
        
        private float GetGravityScale()
        {
            if (_rb.velocity.y <= 0) {
                return gravity;
            }
            return Input.GetButton(jumpButton) ? longJumpGravity : jumpGravity;
        }

        public void Jump()
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpQueued = false;
        }
        
        private bool CanJump() => _jumpQueued && _jumpQueueTimer < jumpQueueSeconds && _grounded.IsGrounded();
        private void FixedUpdate()
        {
            if (CanJump())
            {
                Jump();
            }
            _rb.gravityScale = GetGravityScale();
        }
    }
}
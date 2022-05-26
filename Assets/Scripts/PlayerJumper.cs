using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D), typeof(GroundedController))]
    public class PlayerJumper : MonoBehaviour, IStunnable
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpQueueSeconds;
        [SerializeField] private string jumpButton = "Jump";

        [Header("Gravity")] 
        [SerializeField] private float groundGravity;
        [SerializeField] private float fallGravity;
        [SerializeField] private float jumpGravity;
        [SerializeField] private float longJumpGravity;
        
        public bool IsStunned { get; set; }

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

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
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
using UnityEngine;

namespace FogFormer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : Mover
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
        public override int MoveDirection => MoveInput.x < 0 ? -1 : (MoveInput.x > 0 ? 1 : 0);
        public int LookDirection { get; private set; } = 1;

        public override bool IsMoving => !IsStunned && MoveInput.x != 0;

        public Vector2 Velocity => _rb.velocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _grounded = GetComponent<GroundedController>();
        }

        private void Update()
        {
            if (IsStunned)
            {
                MoveInput = Vector2.zero;
                return;
            }
            MoveInput = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            if (MoveInput.x != 0) LookDirection = MoveDirection;
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

        private Vector2 _velocityToSet;
        private void FixedUpdate()
        {
            if (_velocityToSet != Vector2.zero)
            {
                _rb.velocity = _velocityToSet;
                _velocityToSet = Vector2.zero;
                return;
            }
            
            if (IsStunned)
            {
                return;
            }
            float velocityX = Mathf.MoveTowards(_rb.velocity.x, speed * MoveInput.x, VelocityDelta);
            if (!_grounded.IsGrounded)
            {
                _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
                return;
            }
            _rb.velocity = -(_grounded.GroundSlope) * velocityX;
        }

        public override void SetVelocity(Vector2 velocity)
        {
            _velocityToSet = velocity;
        }
    }

}

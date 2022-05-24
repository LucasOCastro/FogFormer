using UnityEngine;

namespace FogFormer.AI
{
    [RequireComponent(typeof(Rigidbody2D), typeof(FlooredMover))]
    public class FlooredMover : Mover
    {
        [SerializeField] private float speed;
        [SerializeField] private float horizontalDistanceTolerance = .1f;
        [SerializeField] private float fallAvoidDistance;
        
        private GroundedController _grounded;
        private Collider2D _collider;
        private Rigidbody2D _rb;
        protected override void Awake()
        {
            base.Awake();
            _grounded = GetComponent<GroundedController>();
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public override bool IsMoving => base.IsMoving && _grounded.IsGrounded;

        public override bool ReachedTarget => Mathf.Abs(Target.x - _rb.position.x) < horizontalDistanceTolerance;
        
        public override bool CanReachTarget
        {
            get
            {
                Vector2 position = _rb.position;
                Vector2 direction = Mathf.Sign(Target.x - position.x) * -_grounded.GroundSlope;
                return _grounded.WouldBeGroundedAt(position + direction * fallAvoidDistance);
            }
        }

        //This would really be better with a surface system but no time
        protected override void MoveToTarget()
        {
            if (!_grounded.IsGrounded)
            {
                _rb.gravityScale = 1;
                return;
            }
            _rb.gravityScale = 0;
            
            Vector2 position = _rb.position;
            Vector2 direction = Mathf.Sign(Target.x - position.x) * -_grounded.GroundSlope;
            //TODO remove
            Debug.DrawRay(position + direction * fallAvoidDistance, Vector2.down * 2, Color.red);

            if (!CanReachTarget)
            {
                return;
            }
            _rb.position = position + (direction * speed * Time.deltaTime);
        }
    }
}
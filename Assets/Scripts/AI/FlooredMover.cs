using UnityEngine;

namespace FogFormer.AI
{
    [RequireComponent(typeof(Rigidbody2D), typeof(FlooredMover))]
    public class FlooredMover : TargetedMover
    {
        [SerializeField] private float speed;
        [SerializeField] private float fallAvoidDistance;
        
        private GroundedController _grounded;
        private Rigidbody2D _rb;
        protected override void Awake()
        {
            base.Awake();
            _grounded = GetComponent<GroundedController>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void SetVelocity(Vector2 velocity)
        {
            _rb.velocity = velocity;
        }

        public override bool IsMoving => base.IsMoving && _grounded.IsGrounded;

        public override bool HasReachedTarget() => Mathf.Approximately(Target.x, _rb.position.x);
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
                return;
            }

            Vector2 position = _rb.position;
            Vector2 direction = Mathf.Sign(Target.x - position.x) * -_grounded.GroundSlope;
            if (!CanReachTarget)
            {
                return;
            }
            _rb.position = position + (direction * speed * Time.deltaTime);
        }
    }
}
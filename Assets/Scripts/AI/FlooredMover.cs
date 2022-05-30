using UnityEngine;

namespace FogFormer.AI
{
    [RequireComponent(typeof(Rigidbody2D), typeof(FlooredMover))]
    public class FlooredMover : TargetedMover
    {
        [SerializeField] private float speed, turnSeconds;
        [SerializeField] private float fallAvoidDistance;
        
        private GroundedController _grounded;
        private Rigidbody2D _rb;
        protected override void Awake()
        {
            base.Awake();
            _grounded = GetComponent<GroundedController>();
            _rb = GetComponent<Rigidbody2D>();
            _dir = GetComponent<DirectionManager>().Direction;
        }

        public override void SetVelocity(Vector2 velocity)
        {
            _rb.velocity = velocity;
        }

        public override bool IsMoving => base.IsMoving && _grounded.IsGrounded && _dir == TargetDir;

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

        public override int MoveDirection => _dir;
        
        private int TargetDir => (Target.x < _rb.position.x) ? -1 : 1;

        private float _turnTimer;
        private int _dir;
        //This would really be better with a surface system but no time
        protected override void MoveToTarget()
        {
            if (!_grounded.IsGrounded)
            {
                return;
            }

            if (!CanReachTarget)
            {
                return;
            }

            int targetDir = TargetDir;
            if (_dir != targetDir)
            {
                _turnTimer += Time.deltaTime;
                if (_turnTimer < turnSeconds)
                {
                    return;
                }
            }

            _dir = targetDir;
            _turnTimer = 0;
            _rb.position += Vector2.right * _dir * speed * Time.deltaTime;
        }
    }
}
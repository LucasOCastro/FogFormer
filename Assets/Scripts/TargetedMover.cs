using UnityEngine;

namespace FogFormer
{
    public abstract class TargetedMover : Mover
    {
        protected bool _shouldMove;
        public Vector2 Target { get; protected set; }
        protected virtual void Awake()
        {
            ClearTarget();
        }

        public void SetTarget(Vector2 newTarget)
        {
            Target = newTarget;
            _shouldMove = true;
        }
        public void ClearTarget()
        {
            Target = transform.position;
            _shouldMove = false;
        }

        public abstract bool HasReachedTarget();
        public abstract bool CanReachTarget { get; }
        public override bool IsMoving => _shouldMove;
        
        public override int MoveDirection
        {
            get
            {
                float xDif = Target.x - transform.position.x;
                if (xDif < 0) return -1;
                return 1;
            }
        }

        protected abstract void MoveToTarget();

        protected virtual void Update()
        {
            if (_shouldMove && !IsStunned)
            {
                MoveToTarget();
            }

            if (HasReachedTarget())
            {
                ClearTarget();
            }
        }

    }
}
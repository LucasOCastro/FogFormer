using UnityEngine;

namespace FogFormer
{
    public abstract class Mover : MonoBehaviour, IStunnable
    {
        public bool IsStunned { get; set; }
        
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

        public abstract bool HasReachedTarget(float distance = 0);
        public abstract bool CanReachTarget { get; }
        public virtual bool IsMoving => _shouldMove;
        
        public virtual int Direction
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
        }

        
    }
}
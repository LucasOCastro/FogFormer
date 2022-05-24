using UnityEngine;

namespace FogFormer
{
    public abstract class Mover : MonoBehaviour
    {
        public Vector2 Target { get; protected set; }
        protected virtual void Awake()
        {
            Target = transform.position;
        }

        public virtual void SetTarget(Vector2 newTarget)
        {
            Target = newTarget;
        }

        public virtual bool ReachedTarget => Target == (Vector2)transform.position;
        public virtual bool IsMoving => !ReachedTarget;
        public virtual bool CanReachTarget => true;

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
            if (!ReachedTarget)
            {
                MoveToTarget();
            }
            else ClearTarget();
        }

        public void ClearTarget()
        {
            Target = transform.position;
        }
    }
}
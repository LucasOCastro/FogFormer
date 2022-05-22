using System;
using UnityEngine;

namespace FogFormer.AI
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
        protected abstract void MoveToTarget();

        protected virtual void Update()
        {
            if (!ReachedTarget)
            {
                MoveToTarget();
            }
        }
    }
}
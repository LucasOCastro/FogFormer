using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace FogFormer
{
    public class SimpleMover : TargetedMover
    {
        [SerializeField] private float speed;
        
        protected override void MoveToTarget()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        }

        public override bool HasReachedTarget(float distance = 0) => (Target - (Vector2)transform.position).sqrMagnitude <= distance * distance;

        public override void SetVelocity(Vector2 velocity)
        {
        }

        public override bool CanReachTarget => true;
    }
}
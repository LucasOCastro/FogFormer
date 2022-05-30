using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class HasLineOfSightNode : LeafNode
    {
        [SerializeField] private int targetIndex;
        [SerializeField] private LayerMask obstacleMask;
        public override NodeState Tick(BehaviorRunData data)
        {
            Vector2 origin = data.runner.transform.position;
            Vector2 target = data.targets[targetIndex].position;
            float distance = (target - origin).magnitude;
            Vector2 direction = (target - origin) / distance;
            if (Physics2D.Raycast(origin, direction, distance, obstacleMask))
            {
                return NodeState.Failure;
            }
            return NodeState.Success;
        }
    }
}
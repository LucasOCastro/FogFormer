using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class InRangeNode : LeafNode
    {
        [SerializeField] private float range;
        [SerializeField] private int targetIndex;
        public override NodeState Tick(BehaviorRunData data)
        {
            Vector2 target = data.targets[targetIndex].position;
            float sqrDistance = (target - (Vector2)data.runner.transform.position).sqrMagnitude;
            return (sqrDistance < range * range) ? NodeState.Success : NodeState.Failure;
        }
    }
}
using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class IsDeadNode : LeafNode
    {
        [SerializeField] private int targetIndex;
        public override NodeState Tick(BehaviorRunData data)
        {
            if (data.targets[targetIndex] == null)
            {
                return NodeState.Failure;
            }
            var health = data.targets[targetIndex].GetComponent<HealthManager>();
            if (health != null && health.Health == 0)
            {
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
    }
}
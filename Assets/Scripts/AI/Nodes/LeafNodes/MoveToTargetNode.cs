using System;
using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class MoveToTargetNode : LeafNode
    {
        [SerializeField] private int targetIndex;
        public override NodeState Tick(BehaviorRunData data)
        {
            if (targetIndex < 0 || targetIndex >= data.targets.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(targetIndex));
            }
            
            var mover = data.runner.GetComponent<Mover>();
            
            Vector2 target = data.targets[targetIndex].position;
            if (mover.Target != target)
            {
                mover.SetTarget(target);
            }

            return mover.ReachedTarget ? NodeState.Success : NodeState.Running;
        }
    }
}
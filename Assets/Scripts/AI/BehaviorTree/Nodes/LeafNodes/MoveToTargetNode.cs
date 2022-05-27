using System;
using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class MoveToTargetNode : ComplexLeafNode
    {
        //I should probably move this onto a base class?
        [SerializeField] protected int targetIndex;
        [SerializeField] private float distance;

        private TargetedMover _mover;
        protected override NodeState OnStart(BehaviorRunData data)
        {
            if (_mover == null && !data.runner.TryGetComponent(out _mover))
            {
                return NodeState.Failure;
            }
            
            Vector2 target = data.targets[targetIndex].position;
            _mover.SetTarget(target);
            return NodeState.Running;
        }

        protected override NodeState OnTick(BehaviorRunData data)
        {
            if (!_mover.CanReachTarget)
            {
                return NodeState.Failure;
            }
            if (_mover.HasReachedTarget(distance))
            {
                return NodeState.Success;
            }
            return NodeState.Running;
        }

        protected override void OnEnd(BehaviorRunData data, NodeState endState)
        {
            _mover.ClearTarget();
        }
    }
}
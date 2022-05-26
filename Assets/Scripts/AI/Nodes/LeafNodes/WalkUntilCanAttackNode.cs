using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class WalkUntilCanAttackNode : MoveToTargetNode
    {
        private AIAttacker _attacker;
        private HealthManager _target;
        protected override NodeState OnStart(BehaviorRunData data)
        {
            var baseState = base.OnStart(data);
            if (baseState != NodeState.Running)
            {
                return baseState;
            }
            
            if (_attacker == null && !data.runner.TryGetComponent(out _attacker))
            {
                return NodeState.Failure;
            }
            
            if (_target == null && !data.targets[targetIndex].TryGetComponent(out _target))
            {
                return NodeState.Failure;
            }

            return NodeState.Running;
        }

        protected override NodeState OnTick(BehaviorRunData data)
        {
            if (_attacker.HasValidAttackFor(_target))
            {
                return NodeState.Success;
            }
            return base.OnTick(data);
        }
    }
}
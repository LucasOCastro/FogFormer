using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class AttackNode : ComplexLeafNode
    {
        [SerializeField] private int targetIndex;

        private AIAttacker _attacker;
        private HealthManager _target;

        protected override NodeState OnStart(BehaviorRunData data)
        {
            if (_attacker == null && !data.runner.TryGetComponent(out _attacker))
            {
                return NodeState.Failure;
            }
            
            if (_target == null && !data.targets[targetIndex].TryGetComponent(out _target))
            {
                return NodeState.Failure;
            }

            return _attacker.TryTriggerValidAttack(_target) ? NodeState.Running : NodeState.Failure;
        }

        protected override NodeState OnTick(BehaviorRunData data)
        {
            return _attacker.IsAttacking ? NodeState.Running : NodeState.Success;
        }

        protected override void OnEnd(BehaviorRunData data)
        {
        }
    }
}
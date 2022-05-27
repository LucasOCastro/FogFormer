namespace FogFormer.AI.Nodes
{
    public class StopMovementNode : LeafNode
    {
        public override NodeState Tick(BehaviorRunData data)
        {
            TargetedMover mover = data.runner.GetComponent<TargetedMover>();
            if (mover == null)
            {
                return NodeState.Failure;
            }
            mover.ClearTarget();
            return NodeState.Success;

        }
    }
}
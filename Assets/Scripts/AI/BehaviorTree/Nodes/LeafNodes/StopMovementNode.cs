namespace FogFormer.AI.Nodes
{
    public class StopMovementNode : LeafNode
    {
        public override NodeState Tick(BehaviorRunData data)
        {
            Mover mover = data.runner.GetComponent<Mover>();
            if (mover == null)
            {
                return NodeState.Failure;
            }
            mover.ClearTarget();
            return NodeState.Success;

        }
    }
}
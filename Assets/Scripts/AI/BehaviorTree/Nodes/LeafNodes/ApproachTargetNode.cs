namespace FogFormer.AI.Nodes
{
    public class ApproachTargetNode : MoveToTargetNode
    {
        protected override NodeState OnTick(BehaviorRunData data)
        {
            var state = base.OnTick(data);
            return (state == NodeState.Failure) ? state : NodeState.Success;
        }

        protected override void OnEnd(BehaviorRunData data, NodeState endState)
        {
            if (endState == NodeState.Failure)
            {
                base.OnEnd(data, endState);
            }
        }
    }
}
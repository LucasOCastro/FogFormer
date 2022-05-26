namespace FogFormer.AI.Nodes
{
    public class InverterNode : DecoratorNode
    {
        protected override NodeState Decorate(NodeState state) => state switch
        {
            NodeState.Success => NodeState.Failure,
            NodeState.Failure => NodeState.Success,
            _ => state
        };
    }
}
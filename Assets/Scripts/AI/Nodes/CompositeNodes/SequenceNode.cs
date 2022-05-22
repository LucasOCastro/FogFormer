namespace FogFormer.AI.Nodes
{
    public class SequenceNode : CompositeNode
    {
        public override NodeState Tick(BehaviorRunData data)
        {
            for (int i  = StartingIndex; i < children.Count; i++)
            {
                var state = children[i].Tick(data);
                switch (state)
                {
                    case NodeState.Failure: 
                        return state;
                    case NodeState.Running:
                        ReportRunning(i);
                        return state;
                }
            }
            return NodeState.Success;
        }
    }
}
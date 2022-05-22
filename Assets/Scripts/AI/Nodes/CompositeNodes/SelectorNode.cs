namespace FogFormer.AI.Nodes
{
    public class SelectorNode : CompositeNode
    {
        public override NodeState Tick()
        {
            for (int i  = StartingIndex; i < children.Count; i++)
            {
                var state = children[i].Tick();
                switch (state)
                {
                    case NodeState.Success: 
                        return state;
                    case NodeState.Running:
                        ReportRunning(i);
                        return state;
                }
            }
            return NodeState.Failure;
        }
    }
}
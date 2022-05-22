using System;

namespace FogFormer.AI.Nodes
{
    public class SequenceNode : CompositeNode
    {
        public override NodeState Tick()
        {
            for (int i  = StartingIndex; i < _children.Length; i++)
            {
                var state = _children[i].Tick();
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
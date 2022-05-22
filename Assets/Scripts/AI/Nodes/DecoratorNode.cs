namespace FogFormer.AI.Nodes
{
    public abstract class DecoratorNode : Node
    {
        [UnityEngine.SerializeField] private Node _child;
        public Node Child => _child;
        
        protected abstract NodeState Decorate(NodeState state);
        
        public override NodeState Tick()
        {
            return Decorate(_child.Tick());
        }
    }
}
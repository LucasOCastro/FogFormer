using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector]
        [SerializeField] private Node child;
        public Node Child => child;
        
        protected abstract NodeState Decorate(NodeState state);
        
        public override NodeState Tick()
        {
            return Decorate(child.Tick());
        }

        public override Node Clone()
        {
            var clone = Instantiate(this);
            clone.child = child.Clone();
            return clone;
        }
    }
}
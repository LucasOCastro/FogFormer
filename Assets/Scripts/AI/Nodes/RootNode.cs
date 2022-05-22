using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class RootNode : Node
    {
        [HideInInspector]
        [SerializeField] private Node child;

        public override NodeState Tick()
        {
            return child.Tick();
        }
        
        public override Node Clone()
        {
            var clone = Instantiate(this);
            clone.child = child.Clone();
            return clone;
        }
    }
}
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
    }
}
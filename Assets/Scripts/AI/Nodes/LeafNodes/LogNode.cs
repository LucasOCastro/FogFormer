using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class LogNode : LeafNode
    {
        [SerializeField] private string message;
        public override NodeState Tick()
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}
using UnityEditor.VersionControl;
using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public class DebugLogStateNode : DecoratorNode
    {
        [SerializeField]private string extraMessage;
        protected override NodeState Decorate(NodeState state)
        {
            Debug.Log(extraMessage + state);
            return state;
        }
    }
}
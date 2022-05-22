using FogFormer.AI.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = FogFormer.AI.Nodes.Node;

namespace FogFormer.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public System.Action<Node> OnNodeSelected;
        
        public Port input, output;
        public Node node;
        public NodeView(Node node)
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid;
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            if (node is RootNode) return;
            
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            inputContainer.Add(input);
        }

        private void CreateOutputPorts()
        {
            if (node is LeafNode) return;
            
            var capacity = (node is CompositeNode) ? Port.Capacity.Multi : Port.Capacity.Single;
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));
            output.portName = "";
            outputContainer.Add(output);
        }

        public override bool IsSelectable() => base.IsSelectable() && node is not RootNode;

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position = new Vector2(newPos.x, newPos.y);
        }

        public override void OnSelected()
        {
            OnNodeSelected?.Invoke(node);
        }
    }
}
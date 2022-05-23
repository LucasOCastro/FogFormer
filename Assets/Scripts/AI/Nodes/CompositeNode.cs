using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FogFormer.AI.Nodes
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] protected List<Node> children = new List<Node>();

        private int _runningNodeIndex;
        protected void ReportRunning(int index)
        {
            _runningNodeIndex = index;
        }

        protected int StartingIndex
        {
            get
            {
                //TODO This could default to 0 for simplicity
                if (_runningNodeIndex < 0)
                {
                    return 0;
                }
                int index = _runningNodeIndex;
                _runningNodeIndex = -1;
                return index;
            }
        }

        public override Node Clone()
        {
            var clone = Instantiate(this);
            clone.children = children.ConvertAll(n => n.Clone());
            return clone;
        }
    }
}
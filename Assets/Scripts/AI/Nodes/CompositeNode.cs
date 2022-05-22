using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FogFormer.AI.Nodes
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] 
        [SerializeField] protected List<Node> children;

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
    }
}
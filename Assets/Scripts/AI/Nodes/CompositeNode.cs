using System.Collections.Generic;

namespace FogFormer.AI.Nodes
{
    public abstract class CompositeNode : Node
    {
        [UnityEngine.SerializeField] protected Node[] _children;

        /*private int _currentProcessedIndex;
        protected IEnumerable<Node> Children
        {
            get
            {
                _currentProcessedIndex = 0;
                for (_currentProcessedIndex = 0; _currentProcessedIndex < _children.Length; _currentProcessedIndex++)
                {
                    yield return _children[_currentProcessedIndex];
                }
            }
        }*/

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
using UnityEngine;
namespace FogFormer.AI.Nodes
{
    public class WaitNode : LeafNode
    {
        [SerializeField] private float seconds = 1;
        private float _timer;
        public override NodeState Tick()
        {
            if (_timer < seconds)
            {
                _timer += Time.deltaTime;
                return NodeState.Running;
            }
            _timer = 0;
            return NodeState.Success;            
        }
    }
}
using UnityEngine;
using FogFormer.AI.Nodes;
namespace FogFormer.AI
{
    public class BehaviorTree : ScriptableObject
    {
        [SerializeField] private Node _rootNode;
        public void Tick()
        {
            _rootNode.Tick();
        }
    }
}
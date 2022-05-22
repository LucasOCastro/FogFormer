using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public abstract class Node : UnityEngine.ScriptableObject
    {
        public abstract NodeState Tick();
        
#if UNITY_EDITOR
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
#endif
    }
}
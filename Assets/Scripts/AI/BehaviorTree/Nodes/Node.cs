using UnityEngine;

namespace FogFormer.AI.Nodes
{
    public abstract class Node : UnityEngine.ScriptableObject
    {
        public abstract NodeState Tick(BehaviorRunData data);

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        
#if UNITY_EDITOR
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
#endif
    }
}
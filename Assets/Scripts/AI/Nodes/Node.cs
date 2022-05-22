namespace FogFormer.AI.Nodes
{
    [System.Serializable]
    public abstract class Node
    {
        public BehaviorTree ParentTree { get; }
        public abstract NodeState Tick();
    }
}
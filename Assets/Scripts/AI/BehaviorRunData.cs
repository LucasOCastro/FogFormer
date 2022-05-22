using UnityEngine;

namespace FogFormer.AI
{
    [System.Serializable]
    public struct BehaviorRunData
    {
        [HideInInspector]
        public BehaviorTreeRunner runner;
        public Transform[] targets;
    }
}
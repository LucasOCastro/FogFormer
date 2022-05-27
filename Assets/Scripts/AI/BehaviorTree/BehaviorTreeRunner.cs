using UnityEngine;

namespace FogFormer.AI
{
    public class BehaviorTreeRunner : MonoBehaviour, IStunnable
    {
        [SerializeField] private BehaviorTree tree;
        [SerializeField] private BehaviorRunData data;

        public bool IsStunned { get; set; }
        
        private void Awake()
        {
            tree = tree.Clone();
            data.runner = this;
        }

        private void Update()
        {
            if (IsStunned) return;
            
            tree.Tick(data);
        }
    }
}
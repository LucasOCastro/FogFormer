using UnityEngine;

namespace FogFormer.AI
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTree tree;
        [SerializeField] private BehaviorRunData data;

        private void Awake()
        {
            tree = tree.Clone();
            data.runner = this;
        }

        private void Update()
        {
            tree.Tick(data);
        }
    }
}
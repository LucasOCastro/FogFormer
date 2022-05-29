using System;
using UnityEngine;

namespace FogFormer.AI
{
    public class BehaviorTreeRunner : MonoBehaviour, IStunnable
    {
        [SerializeField] private BehaviorTree tree;
        [SerializeField] private BehaviorRunData data;

        [SerializeField] private bool assignPlayerAsTarget0;

        public bool IsStunned { get; set; }
        
        private void Awake()
        {
            tree = tree.Clone();
            data.runner = this;
        }
        
        private void Start()
        {
            if (assignPlayerAsTarget0)
            {
                if (data.targets == null || data.targets.Length == 0)
                {
                    data.targets = new Transform[1];
                }
                data.targets[0] = GameManager.Instance.Player.transform;
            }
        }

        private void Update()
        {
            if (IsStunned) return;
            
            tree.Tick(data);
        }
    }
}
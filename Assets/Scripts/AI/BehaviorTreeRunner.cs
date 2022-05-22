using System;
using UnityEngine;

namespace FogFormer.AI
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTree tree;
        private void Awake()
        {
            tree = tree.Clone();
        }

        private void Update()
        {
            tree.Tick();
        }
    }
}
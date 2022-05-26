using UnityEngine;

namespace FogFormer.AI
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTree tree;
        [SerializeField] private BehaviorRunData data;
        
        //DEBUG
        [SerializeField] private FogFormer.AI.Nodes.Node runningNode;

        private static System.Action<FogFormer.AI.Nodes.Node> hnjgfk;
        public static void ReportRunning(FogFormer.AI.Nodes.Node rn)
        {
            hnjgfk?.Invoke(rn);
        }
        
        private void Awake()
        {
            hnjgfk += a => runningNode = a;
            
            tree = tree.Clone();
            data.runner = this;
        }

        private void Update()
        {
            tree.Tick(data);
        }
    }
}
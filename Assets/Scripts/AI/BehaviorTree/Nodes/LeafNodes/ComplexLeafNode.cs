namespace FogFormer.AI.Nodes
{
    public abstract class ComplexLeafNode : LeafNode
    {
        private bool _running;
        public override NodeState Tick(BehaviorRunData data)
        {
            if (!_running)
            {
                var state = OnStart(data);
                if (state == NodeState.Running) _running = true;
                return state;
            }

            var runState = OnTick(data);
            if (runState != NodeState.Running)
            {
                _running = false;
                OnEnd(data, runState);
            }
            return runState;
        }

        protected abstract NodeState OnTick(BehaviorRunData data);
        protected abstract NodeState OnStart(BehaviorRunData data);
        protected abstract void OnEnd(BehaviorRunData data, NodeState endState);
    }
}
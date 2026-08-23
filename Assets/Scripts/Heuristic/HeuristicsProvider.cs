using EasyField.Heuristic.Functions;
using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public abstract class HeuristicsProvider<TNodeData> : IHeuristicsProvider<TNodeData>
        where TNodeData : INodeData
    {
        private readonly IHeuristicFunction _heuristicFunction;
        private readonly float _minStepCost;


        public HeuristicsProvider(IHeuristicFunction heuristicFunction, float minStepCost)
        {
            _heuristicFunction = heuristicFunction;
            _minStepCost = minStepCost;
        }

        public float EstimateCost(TNodeData nodeData1, TNodeData nodeData2)
        {
            return _heuristicFunction.Estimate(nodeData1.NodePosition, nodeData2.NodePosition) * _minStepCost;
        }
    }
}
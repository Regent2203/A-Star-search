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

        public float EstimateCost(TNodeData from, TNodeData to)
        {
            return _heuristicFunction.Estimate(from.NodePosition, to.NodePosition) * _minStepCost;
        }
    }
}
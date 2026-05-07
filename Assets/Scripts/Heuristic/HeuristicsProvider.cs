using Core.Nodes;

namespace Core.Heuristic
{
    public abstract class HeuristicsProvider : IHeuristicsProvider
    {
        private readonly IHeuristicFunction _heuristicFunction;        
        private readonly float _minStepCost;


        public HeuristicsProvider(IHeuristicFunction heuristicFunction, float minStepCost)
        {
            _heuristicFunction = heuristicFunction;
            _minStepCost = minStepCost;
        }

        public float EstimateCost(INode node1, INode node2)
        {
            return _heuristicFunction.Estimate(node1.Position, node2.Position) * _minStepCost;
        }
    }
}
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

        public float EstimateCost(IEstimatable node1, IEstimatable node2)
        {
            return _heuristicFunction.Estimate(node1.GetEstimatedPosition(), node2.GetEstimatedPosition()) * _minStepCost;
        }
    }
}
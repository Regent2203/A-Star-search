using Core.Nodes;

namespace Core.Heuristic
{
    public class HeuristicsProvider : IHeuristicsProvider
    {
        private IHeuristicFunction _heuristicFunction;
        private float _minStepCost = float.PositiveInfinity;


        public HeuristicsProvider(IHeuristicFunction heuristicFunction)
        {
            _heuristicFunction = heuristicFunction;
        }

        public void SetMinimumStepCost(float value, bool forced)
        {
            if (forced)
            {
                _minStepCost = value;
            }
            else 
            if (value < _minStepCost)
                _minStepCost = value;
        }

        public float EstimateCost(IEstimatable node1, IEstimatable node2)
        {
            return _heuristicFunction.EstimateCost(node1, node2) * _minStepCost;
        }
    }
}

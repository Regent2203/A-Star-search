using Core.Nodes;

namespace Core.Heuristic
{
    public class HeuristicsProvider : IHeuristicsProvider
    {
        private IHeuristicFunction _heuristicFunction;
        private float _minStepCost = 1.0f;


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
            var p1 = node1.GetEstimatedPosition();
            var p2 = node2.GetEstimatedPosition();

            return _heuristicFunction.Estimate(p1, p2) * _minStepCost;
        }
    }
}

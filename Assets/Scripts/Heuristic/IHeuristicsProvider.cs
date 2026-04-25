using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider
    {
        public void SetMinimumStepCost(float value, bool forced);
        public float EstimateCost(IEstimatable node1, IEstimatable node2);
    }
}

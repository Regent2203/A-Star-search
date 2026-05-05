using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider
    {
        public float EstimateCost(IEstimatable node1, IEstimatable node2);
    }
}
using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicFunction
    {
        public float EstimateCost(IEstimatable node1, IEstimatable node2);
    }
}

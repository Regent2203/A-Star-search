using Core.Nodes;

namespace Core.HeuristicFunctions
{
    public interface IHeuristicFunction
    {
        public float EstimateCost(IEstimatable node1, IEstimatable node2);
    }
}

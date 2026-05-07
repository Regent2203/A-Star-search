using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider
    {
        public float EstimateCost(INode node1, INode node2);
    }
}
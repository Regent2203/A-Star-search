using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider<T> where T : class, INode
    {
        public float EstimateCost(T node1, T node2);
    }
}
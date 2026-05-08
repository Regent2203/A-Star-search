using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider<T, TId> where T : class, INode<T, TId>
    {
        public float EstimateCost(T node1, T node2);
    }
}
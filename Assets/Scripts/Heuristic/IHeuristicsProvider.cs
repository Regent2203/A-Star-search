using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider<T, TId> where T : class, INode<TId>
    {
        public float EstimateCost(T node1, T node2);
    }
}
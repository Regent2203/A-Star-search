using Core.Nodes;

namespace Core.Heuristic
{
    public interface IHeuristicsProvider<T> where T : INode<T>
    {
        public float EstimateCost(T node1, T node2);
    }
}
using Core.Nodes;

namespace Core.Heuristic
{
    public class DijkstraHeuristicsProvider<T> : IHeuristicsProvider<T> where T : class, INode
    {
        public float EstimateCost(T node1, T node2)
        {
            return 0.0f;
        }
    }
}

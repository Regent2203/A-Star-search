using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public class DijkstraHeuristicsProvider<T> : IHeuristicsProvider<T> 
        where T : INodeData
    {
        public float EstimateCost(T node1, T node2)
        {
            return 0.0f;
        }
    }
}

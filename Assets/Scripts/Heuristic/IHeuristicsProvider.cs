using ThisProject.Nodes;

namespace ThisProject.Heuristic
{
    public interface IHeuristicsProvider<T>
        where T : INodeData
    {
        public float EstimateCost(T node1, T node2);
    }
}
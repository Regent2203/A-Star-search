using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public interface IHeuristicsProvider<T>
        where T : INodeData
    {
        public float EstimateCost(T node1, T node2);
    }
}
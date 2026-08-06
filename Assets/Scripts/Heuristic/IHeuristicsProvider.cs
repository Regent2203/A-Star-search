using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public interface IHeuristicsProvider<TNodeData>
        where TNodeData : INodeData
    {
        public float EstimateCost(TNodeData nodeData1, TNodeData nodeData2);
    }
}
using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public class DijkstraHeuristicsProvider<TNodeData> : IHeuristicsProvider<TNodeData> 
        where TNodeData : INodeData
    {
        public float EstimateCost(TNodeData from, TNodeData to)
        {
            return 0.0f;
        }
    }
}

using EasyField.Nodes;

namespace EasyField.Heuristic
{
    public class DijkstraHeuristicsProvider<TNodeData> : IHeuristicsProvider<TNodeData> 
        where TNodeData : INodeData
    {
        public float EstimateCost(TNodeData nodeData1, TNodeData nodeData2)
        {
            return 0.0f;
        }
    }
}

using ThisProject.Nodes;

namespace ThisProject.Links.CostProviders
{
    public interface ICostProvider<TNodeData>
        where TNodeData : INodeData
    {
        public float GetCost(TNodeData from, TNodeData to);
    }
}
using ThisProject.Nodes;

namespace ThisProject.Links.CostProviders
{
    public interface IWeightGetter<TNodeData>
        where TNodeData : INodeData
    {
        public float GetWeight(TNodeData source);
    }    
}
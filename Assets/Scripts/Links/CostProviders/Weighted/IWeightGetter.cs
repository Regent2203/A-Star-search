using EasyField.Nodes;

namespace EasyField.Links.CostProviders
{
    public interface IWeightGetter<TNodeData>
        where TNodeData : INodeData
    {
        public float GetWeight(TNodeData source);
    }    
}
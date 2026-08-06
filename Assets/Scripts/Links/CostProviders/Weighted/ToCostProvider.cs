using EasyField.Nodes;

namespace EasyField.Links.CostProviders
{
    public class ToCostProvider<TNodeData> : WeightedCostProvider<TNodeData>
        where TNodeData : INodeData
    {
        public ToCostProvider(IWeightGetter<TNodeData> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(TNodeData from, TNodeData to)
        {
            return _weightGetter.GetWeight(to);
        }
    }
}
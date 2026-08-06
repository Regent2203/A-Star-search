using EasyField.Nodes;

namespace EasyField.Links.CostProviders
{
    public class FromCostProvider<TNodeData> : WeightedCostProvider<TNodeData>
        where TNodeData : INodeData
    {
        public FromCostProvider(IWeightGetter<TNodeData> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(TNodeData from, TNodeData to)
        {
            return _weightGetter.GetWeight(from);
        }
    }
}
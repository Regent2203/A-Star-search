using EasyField.Nodes;

namespace EasyField.Links.CostProviders
{
    public abstract class WeightedCostProvider<TNodeData> : ICostProvider<TNodeData>
        where TNodeData : INodeData
    {
        protected readonly IWeightGetter<TNodeData> _weightGetter;

        public WeightedCostProvider(IWeightGetter<TNodeData> weightGetter)
        {
            _weightGetter = weightGetter;
        }

        protected abstract float GetWeight(TNodeData from, TNodeData to);

        public float GetCost(TNodeData from, TNodeData to)
        {
            return GetWeight(from, to);
        }
    }
}
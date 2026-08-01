using ThisProject.Nodes;

namespace ThisProject.Links.CostProviders
{
    public class AverageCostProvider<TNodeData> : WeightedCostProvider<TNodeData>
        where TNodeData : INodeData
    {
        public AverageCostProvider(IWeightGetter<TNodeData> weightGetter) : base(weightGetter) 
        { }

        protected override float GetWeight(TNodeData from, TNodeData to)
        {
            return (_weightGetter.GetWeight(from) + _weightGetter.GetWeight(to)) * 0.5f;
        }
    }
}
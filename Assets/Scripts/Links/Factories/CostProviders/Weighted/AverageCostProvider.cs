using ThisProject.Nodes;

namespace ThisProject.Links.Factories.CostProviders
{
    public class AverageCostProvider<T> : WeightedCostProvider<T>
        where T : INodeData
    {
        public AverageCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter) 
        { }

        protected override float GetWeight(T from, T to)
        {
            return (_weightGetter.GetWeight(from) + _weightGetter.GetWeight(to)) * 0.5f;
        }
    }
}
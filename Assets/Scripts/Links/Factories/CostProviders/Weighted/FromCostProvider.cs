using ThisProject.Nodes;

namespace ThisProject.Links.Factories.CostProviders
{
    public class FromCostProvider<T> : WeightedCostProvider<T>
        where T : INodeData
    {
        public FromCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(from);
        }
    }
}
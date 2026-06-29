using ThisProject.Nodes;

namespace ThisProject.Links.Factories.CostProviders
{
    public class ToCostProvider<T> : WeightedCostProvider<T>
        where T : INodeData
    {
        public ToCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(to);
        }
    }
}
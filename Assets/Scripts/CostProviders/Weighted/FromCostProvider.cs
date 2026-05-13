using Core.Nodes;

namespace Core.CostProviders
{
    public class FromCostProvider<T> : WeightedCostProvider<T> where T : class, INode
    {
        public FromCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(from);
        }
    }
}
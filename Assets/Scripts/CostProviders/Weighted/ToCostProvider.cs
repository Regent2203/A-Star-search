using Core.Nodes;

namespace Core.CostProviders
{
    public class ToCostProvider<T> : WeightedCostProvider<T> where T : class, INode
    {
        public ToCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(to);
        }
    }
}